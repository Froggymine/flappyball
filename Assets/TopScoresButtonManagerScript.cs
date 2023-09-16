using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TopScoresButtonManagerScript : MonoBehaviour
{
    
    public Button TitleScreenButton;

    void Awake() {
        TitleScreenButton = GameObject.Find("TitleScreenButton").GetComponent<Button>();
    }

    void Start()
    {
       TitleScreenButton.onClick.AddListener(TitleScreen);
    }

    public void TitleScreen() {
        SceneManager.LoadScene("TitleScreen");
    }
}
