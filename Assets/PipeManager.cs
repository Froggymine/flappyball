using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour {
    public GameObject pipe;
    public GameObject ScoreCollider;
    public bool run = true;


    float pipeTime = 1.5f; // Pipe lifetime, default 4.5f
    float destroyMulti = 2.0f; // Multiplier to pipeTime on destroy timer, default 2
    float gapHeight = 3.0f; // Gap between pipes, default 4.0f
    float pipeSpeed = 8.0f; // Pipe left velocity, default 5f

    void Awake() {
        pipe = gameObject.transform.GetChild(0).gameObject;
        ScoreCollider = gameObject.transform.GetChild(1).gameObject;
    }

    void Start() {
        if (StaticClass.Difficulty == "Easy") {
            pipeTime = 3f;
            destroyMulti = 2.0f;
            gapHeight = 4.0f;
            pipeSpeed = 5.0f;
        }
        if (StaticClass.Difficulty == "Medium") {
            pipeTime = 1.5f;
            destroyMulti = 2.0f;
            gapHeight = 3.0f;
            pipeSpeed = 8.0f;
        }
        if (StaticClass.Difficulty == "Hard") {
            pipeTime = 1.8f;
            destroyMulti = 2.0f;
            gapHeight = 2.0f;
            pipeSpeed = 10.0f;
        }
        if (StaticClass.Difficulty == "Extreme") {
            pipeTime = 10.0f;
            destroyMulti = 6.0f;
            gapHeight = 5.0f;
            pipeSpeed = 0.8f;
        }
        PipesStart();
    }

    IEnumerator PipeSpawn() {
        yield return new WaitForSecondsRealtime(2f);
        while (run == true) {
            yield return new WaitForSecondsRealtime(pipeTime); // Wait time before creating pipe
            float heightScale = Random.Range(2f, 6f); // Random generate height scale factor

            // Pipe bottom
            var newPipeBottom = Instantiate(pipe, new Vector2(10, 
                -5.0f+(heightScale/2)
            ), Quaternion.identity); // Create new pipe
            newPipeBottom.transform.localScale = new Vector2(1.5f, heightScale); // Scale pipe to size
            newPipeBottom.GetComponent<Rigidbody2D>().velocity = new Vector2(-pipeSpeed, 0); // Add velocity so it moves left

            // Pipe top
            var newPipeTop = Instantiate(pipe, new Vector2(10, 
                5.0f - (( (10-gapHeight) - heightScale) / 2)
            ), Quaternion.identity); // Create new pipe
            newPipeTop.transform.localScale = new Vector2(1.5f, (10-gapHeight)-heightScale); // Scale pipe to size WITH GAP OF 3 (10-3=7)
            newPipeTop.GetComponent<Rigidbody2D>().velocity = new Vector2(-pipeSpeed, 0); // Add velocity so it moves left

            // Pipe core collider
            var newScoreCollider = Instantiate(ScoreCollider, new Vector2(10, 
                -5.0f + heightScale + (gapHeight/2)
            ), Quaternion.identity); // Create new ScoreCollider in the middle of the gap
            newScoreCollider.transform.localScale = new Vector2(1.5f, gapHeight); // Scale ScoreCollider to the gapSize
            newScoreCollider.GetComponent<Rigidbody2D>().velocity = new Vector2(-pipeSpeed, 0); // Add velocity so it moves left

            if (run == true) {
                Destroy(newPipeBottom,pipeTime*destroyMulti); // Destroy bottom pipe after time
                Destroy(newPipeTop,pipeTime*destroyMulti); // Destroy top pipe after time
                Destroy(newScoreCollider,pipeTime*destroyMulti); // Destroy ScoreCollider after time
            } else {
                // Destroy stuff on game end immediatly, rather than waiting
                Destroy(newPipeBottom); // Destroy bottom pipe
                Destroy(newPipeTop); // Destroy top pipe
                Destroy(newScoreCollider); // Destroy ScoreCollider
            }
        }
    }

    // Message handlers from BirdScript.cs for when game ends
    public void PipesStart() {
        run = true;
        StartCoroutine(PipeSpawn());
    }
    public void PipesEnd() {
        run = false;
    }
}
