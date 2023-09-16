using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public static class StaticClass {
    public static string Difficulty { get; set; }
}

public class ButtonManagerScript : MonoBehaviour
{
    
    public Button EasyButton, MediumButton, HardButton, ExtremeButton, TopScoresButton;

    void Awake() {
        EasyButton = GameObject.Find("EasyButton").GetComponent<Button>();
        MediumButton = GameObject.Find("MediumButton").GetComponent<Button>();
        HardButton = GameObject.Find("HardButton").GetComponent<Button>();
        ExtremeButton = GameObject.Find("ExtremeButton").GetComponent<Button>();
        TopScoresButton = GameObject.Find("TopScoresButton").GetComponent<Button>();
    }

    void Start()
    {
       EasyButton.onClick.AddListener(EasyScene);
       MediumButton.onClick.AddListener(MediumScene);
       HardButton.onClick.AddListener(HardScene);
       ExtremeButton.onClick.AddListener(ExtremeScene);
       TopScoresButton.onClick.AddListener(TopScores);
    }

    public void EasyScene() {
        StaticClass.Difficulty = "Easy";
        SceneManager.LoadScene("Main");
    }
    public void MediumScene() {
        StaticClass.Difficulty = "Medium";
        SceneManager.LoadScene("Main");
    }
    public void HardScene() {
        StaticClass.Difficulty = "Hard";
        SceneManager.LoadScene("Main");
    }
    public void ExtremeScene() {
        StaticClass.Difficulty = "Extreme";
        SceneManager.LoadScene("Main");
    }
    public void TopScores() {
        SceneManager.LoadScene("TopScores");
    }
}
