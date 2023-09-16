using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using TMPro;

public class ScoresManager : MonoBehaviour
{
    [Serializable]
    public class ScoreTemplate {
        public int EasyScore;
        public int MediumScore;
        public int HardScore;
        public int ExtremeScore;
    }

    private TMP_Text EasyScore, MediumScore, HardScore, ExtremeScore;

    private string ScoresFilePath = Application.dataPath.ToString() + "/SaveFiles/TopScores.txt";

    void Awake() {
        EasyScore = GameObject.Find("EasyScore").GetComponent<TMP_Text>();
        MediumScore = GameObject.Find("MediumScore").GetComponent<TMP_Text>();
        HardScore = GameObject.Find("HardScore").GetComponent<TMP_Text>();
        ExtremeScore = GameObject.Find("ExtremeScore").GetComponent<TMP_Text>();
    }

    void Start()
    {
        byte[] jsonFile = File.ReadAllBytes(ScoresFilePath);
        string json = Encoding.ASCII.GetString(jsonFile);
        ScoreTemplate scoreData = JsonUtility.FromJson<ScoreTemplate>(json);

        EasyScore.text = scoreData.EasyScore.ToString();
        MediumScore.text = scoreData.MediumScore.ToString();
        HardScore.text = scoreData.HardScore.ToString();
        ExtremeScore.text = scoreData.ExtremeScore.ToString();
    }
}
