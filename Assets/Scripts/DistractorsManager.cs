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

    //For random genrate
    public float randomTimeMin = 6f;
    public float randomTimeMax = 7f;
    public int randomGenerateNumMin = 2;
    public int randomGenerateNumMax = 5;
    public List<GameObject> allDistractors = new List<GameObject>();
    public bool isRandomGenerate = true;


    private void Update()
    {
        GenerateDistractorsByStage();
        if (isRandomGenerate)
        {
            RandomDistractorsGenerate();
        }
        
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

    private void RandomDistractorsGenerate()
    {
        if(stage >= distractorsStages.Count)
        {
            print("额外随机stage:" + stage);
            time += Time.deltaTime;
            if(time >= Random.Range(randomTimeMin, randomTimeMax))
            {
                int randomGenerate = Random.Range(randomGenerateNumMin, randomGenerateNumMax);
                List<int> randomGNum = new List<int>();
                for(int i = 0; i< randomGenerate; i++)
                {
                    int K = Random.Range(0, allDistractors.Count);
                    if (randomGNum.Contains(K))
                    {
                        i--;
                    }
                    else
                    {
                        randomGNum.Add(K);
                    }
                }
                for(int i = 0; i < randomGNum.Count; i++)
                {
                    GameObject go = Instantiate(allDistractors[randomGNum[i]], allDistractors[randomGNum[i]].GetComponent<Distractor>().pos, Quaternion.identity);
                    go.transform.SetParent(distractorParent.transform);
                }
                stage++;
                time = 0;
            }
           
        }
    }
}
