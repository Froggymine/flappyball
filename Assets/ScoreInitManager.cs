using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;


public class ScoreInitManager : MonoBehaviour
{
    [Serializable]
    public class ScoreTemplate {
        public int EasyScore;
        public int MediumScore;
        public int HardScore;
        public int ExtremeScore;
    }
    private string ScoresFilePath = Application.dataPath.ToString() + "/SaveFiles/TopScores.txt";

    void Start()
    {
        // https://docs.unity3d.com/2020.1/Documentation/Manual/JSONSerialization.html
        if (!File.Exists(ScoresFilePath)) {
            Debug.Log("FileInit");
            ScoreTemplate ScoreTemplateObject = new ScoreTemplate();
            ScoreTemplateObject.EasyScore = 0;
            ScoreTemplateObject.MediumScore = 0;
            ScoreTemplateObject.HardScore = 0;
            ScoreTemplateObject.ExtremeScore = 0;
            string json = JsonUtility.ToJson(ScoreTemplateObject);
            byte[] jsonFile = Encoding.ASCII.GetBytes(json);
            File.WriteAllBytes(ScoresFilePath, jsonFile);
        }
    }
}

