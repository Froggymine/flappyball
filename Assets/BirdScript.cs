using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Text;

public class BirdScript : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Renderer visual;
    private GameObject PipeManager;
    private TMP_Text ScoreDisplay;

    [Serializable]
    public class ScoreTemplate {
        public int EasyScore;
        public int MediumScore;
        public int HardScore;
        public int ExtremeScore;
    }

    private string ScoresFilePath = Application.dataPath.ToString() + "/SaveFiles/TopScores.txt";
    bool gameEnded = false;

    public int score = 0;
    
    void Awake() {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        visual = GetComponent<Renderer>();
        PipeManager = GameObject.Find("PipeManager");
        ScoreDisplay = GameObject.Find("ScoreDisplay").GetComponent<TMP_Text>();
    }

    void Update() {
        if (Input.GetKeyDown("space")) {
            // rb2D.AddForce(Vector2.up * 700);
            rb2D.velocity = new Vector2(0, 5.5f);
        }

        if (Input.GetKeyDown("escape")) {
            SceneManager.LoadScene("TitleScreen");
        }

        if (rb2D.position.y > 6.0f) { // Game end if bird goes above screen
            StartCoroutine(GameEnd());
        }
        if (rb2D.position.y < -6.2f) {
            StartCoroutine(GameEnd()); // Game end if bird goes below screen
        }

        rb2D.velocity = new Vector2(0, rb2D.velocity.y); // Ensure horisontal velocity is 0
    }

    IEnumerator GameEnd() {
        if (gameEnded == false) { // Used to stop constant repeats of GameEnd()
            Debug.Log("Game End");
            PipeManager.SendMessage("PipesEnd"); // Send message to PipeManager.cs to stop pipe spawning
            gameEnded = true; // Used to stop constant repeats of GameEnd()
            rb2D.isKinematic = true; // Stops gravity from affecting bird
            visual.enabled = false; // Hides bird sprite
            rb2D.position = Vector2.zero; // Reset position to middle of screen
            rb2D.velocity = Vector2.zero; // Reset velocity to 0

            byte[] jsonFile = File.ReadAllBytes(ScoresFilePath);
            string json = Encoding.ASCII.GetString(jsonFile);
            ScoreTemplate scoreData = JsonUtility.FromJson<ScoreTemplate>(json);

            if (StaticClass.Difficulty == "Easy") {
                scoreData.EasyScore = score;
            }
            if (StaticClass.Difficulty == "Medium") {
                scoreData.MediumScore = score;
            }
            if (StaticClass.Difficulty == "Hard") {
                scoreData.HardScore = score;
            }
            if (StaticClass.Difficulty == "Extreme") {
                scoreData.ExtremeScore = score;
            }
            string json1 = JsonUtility.ToJson(scoreData);
            byte[] jsonFile1 = Encoding.ASCII.GetBytes(json1);
            File.WriteAllBytes(ScoresFilePath, jsonFile1);

            yield return new WaitForSecondsRealtime(2); // Wait for 2 seconds

            score = 0; // Sets score to 0
            ScoreDisplay.text = score.ToString(); // Updates text display
            rb2D.isKinematic = false; // Let gravity affect bird again
            visual.enabled = true; // Show Bird
            PipeManager.SendMessage("PipesStart"); // Send message to PipeManager.cs to start pipe spawning
            gameEnded = false; // Allow GameEnded() to work again
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name.Contains("Pipe")) { // If bird collides with pipe, then end game
            StartCoroutine(GameEnd());
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.name.Contains("ScoreCollider")) { // If bird collides with ScoreCollider trigger
            if (gameEnded == false) {
                score += 1; // Add one to score
                ScoreDisplay.text = score.ToString(); // Update score display
            }
        }
    }
}
