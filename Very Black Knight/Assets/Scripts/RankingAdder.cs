using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingAdder : MonoBehaviour
{
    public GameObject inputFieldObject;
    InputField field;

    void Start() {
        field = inputFieldObject.GetComponent<InputField>();
    }

    public void addPlayer() {

        PlayerPrefs.SetString("playerName", field.text);
        int score = PlayerPrefs.GetInt("inputCount");
        Debug.LogWarning("Player: "+field.text+" Score: "+score);
        PlayerPrefs.Save();

    }
}
