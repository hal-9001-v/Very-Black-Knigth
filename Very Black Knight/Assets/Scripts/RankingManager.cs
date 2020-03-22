using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    int[] scores;
  
    bool showed = false;
    // Start is called before the first frame update
    void Start()
    {
        scores = new int[5];

        scores[0] = -1;
        scores[1] = -1;
        scores[2] = -1;
        scores[3] = -1;
        scores[4] = -1;

        updateRanking();

        foreach (int a in scores)
        {
            Debug.Log(a);
        }
    }

    void loadRanking(){
        scores[0] = PlayerPrefs.GetInt("score1");
        scores[1] = PlayerPrefs.GetInt("score2");
        scores[2] = PlayerPrefs.GetInt("score3");
        scores[3] = PlayerPrefs.GetInt("score4");
        scores[4] = PlayerPrefs.GetInt("score5");

    }

    void saveRanking() {
        PlayerPrefs.SetInt("score1", scores[0]);
        PlayerPrefs.SetInt("score2", scores[1]);
        PlayerPrefs.SetInt("score3", scores[2]);
        PlayerPrefs.SetInt("score4", scores[3]);
        PlayerPrefs.SetInt("score5", scores[4]);

        PlayerPrefs.Save();
    }

    void updateRanking() {
        //loadRanking();
        System.Array.Sort(scores);
        


    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
