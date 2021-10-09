using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

[RequireComponent(typeof(SpriteRenderer))]
public class GazePointPlot : MonoBehaviour
{
    public float FilterSmoothingFactor = 0.15f;
    private SpriteRenderer gazePointSprite;
	public float VisualizationDistance = 10f;
	private GazePoint _lastGazePoint = GazePoint.Invalid;
	private bool _hasHistoricPoint;
	private Vector3 _historicPoint;
	[SerializeField] Canvas canvas;
	public bool gazePointOnCanvas;
	// Start is called before the first frame update
	void Start()
    {
		gazePointSprite = GetComponent<SpriteRenderer>();
	}

    // Update is called once per frame
    void Update()
    {
		GazePoint gazePoint = TobiiAPI.GetGazePoint();

		if (gazePoint.IsRecent()
			&& gazePoint.Timestamp > (_lastGazePoint.Timestamp + float.Epsilon))
		{
            if (gazePointOnCanvas)
            {
				UpdateGazeBubbleOnCanvas(gazePoint);
            }
            else
            {
				UpdateGazeBubblePosition(gazePoint);
			}
			_lastGazePoint = gazePoint;
		}
	}

	private void UpdateGazeBubblePosition(GazePoint gazePoint)
	{
		Vector3 gazePointInWorld = ProjectToPlaneInWorld(gazePoint);
		transform.position = Smoothify(gazePointInWorld);
	}

	private void UpdateGazeBubbleOnCanvas(GazePoint gazePoint)
    {
		transform.localPosition = ProjectToCanvas(gazePoint);
	}

	private Vector3 ProjectToPlaneInWorld(GazePoint gazePoint)
	{
		Vector3 gazeOnScreen = gazePoint.Screen;
		gazeOnScreen += (transform.forward * VisualizationDistance);
		return Camera.main.ScreenToWorldPoint(gazeOnScreen);
	}

	private Vector3 ProjectToCanvas(GazePoint gazePoint)
    {
		Vector2 viewPortEyePos = gazePoint.Viewport - new Vector2(0.5f, 0.5f);
		Vector3 eyePos = viewPortEyePos * new Vector2(canvas.renderingDisplaySize.x / canvas.scaleFactor, canvas.renderingDisplaySize.y / canvas.scaleFactor);
		return eyePos;
    }

	private Vector3 Smoothify(Vector3 point)
	{
		if (!_hasHistoricPoint)
		{
			_historicPoint = point;
			_hasHistoricPoint = true;
		}

		var smoothedPoint = new Vector3(
			point.x * (1.0f - FilterSmoothingFactor) + _historicPoint.x * FilterSmoothingFactor,
			point.y * (1.0f - FilterSmoothingFactor) + _historicPoint.y * FilterSmoothingFactor,
			point.z * (1.0f - FilterSmoothingFactor) + _historicPoint.z * FilterSmoothingFactor);

		_historicPoint = smoothedPoint;

		return smoothedPoint;
	}
}
