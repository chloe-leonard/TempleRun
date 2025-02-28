using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;
    TimeCount timer;
    GameUIManager uiManager;
    private int score;
    private float time;
    ScoreManager scoreManager;
    void Start()
    {
        scoreManager = GameObject.Find("Canvas").GetComponent<ScoreManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Stop timer
            timer = FindFirstObjectByType<TimeCount>();
            if (timer != null)
            {
                float finalTime = timer.GetElapsedTime();
                timer.SetRunning(false);
                Debug.Log("Temps final: " + finalTime);
            }
            else
            {
                Debug.LogError("TimeCount script not found!");
            }

            // Stop character movement
            playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.canMove = false;
            }
            else
            {
                Debug.LogError("PlayerMovement component not found on the player!");
            }

            // Launch animation
            animator = other.gameObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("Stumble");
            }
            else
            {
                Debug.LogError("Animator component not found on the player!");
            }

            // Reload / Show results
            uiManager = FindFirstObjectByType<GameUIManager>();
            if (uiManager != null && scoreManager != null)
            {
                StartCoroutine(waitAndReload(scoreManager.GetScore(), timer != null ? timer.GetElapsedTime() : 0));
            }
            else
            {
                Debug.LogError("GameUIManager or ScoreManager is missing!");
            }

            if (uiManager != null)
            {

                Debug.Log("✅ uiManager trouvé !");
                StartCoroutine(waitAndReload(scoreManager.GetScore(), timer != null ? timer.GetElapsedTime() : 0));
            }
            else
            {
                Debug.LogError("❌ GameUIManager est NULL !");
            }

        }
    }


    IEnumerator waitAndReload(int score, float finalTime)
    {
        yield return new WaitForSeconds(3f);
        uiManager.ShowResults(score, finalTime);
    }

}
