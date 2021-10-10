using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractorsManager : MonoBehaviour
{
    [System.Serializable]
    public struct distractorsStage
    {
        public float generateTimeMin;
        public float generateTimeMax;
        public List<GameObject> generateDistractors;
    }
    public List<distractorsStage> distractorsStages;
    public int stage = 0;
    public float time = 0;
    [SerializeField] private GameObject distractorParent;
    private void Update()
    {
        GenerateDistractorsByStage();
    }

    private void GenerateDistractorsByStage()
    {
        time += Time.deltaTime;
        if(stage < distractorsStages.Count)
        {
            if (time >= Random.Range(distractorsStages[stage].generateTimeMin, distractorsStages[stage].generateTimeMax))
            {
                for (int i = 0; i < distractorsStages[stage].generateDistractors.Count; i++)
                {
                    GameObject go = Instantiate(distractorsStages[stage].generateDistractors[i], distractorsStages[stage].generateDistractors[i].GetComponent<Distractor>().pos, Quaternion.identity);
                    go.transform.SetParent(distractorParent.transform);
                }
                stage++;
                time = 0;
            }
        }
       
    }
}
