using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;
    private TimeCount timer;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Stop timer
            timer = FindFirstObjectByType<TimeCount>();

            float finalTime = timer.GetElapsedTime();
            timer.SetRunning(false);

            Debug.Log("Temps final: " + finalTime);

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

            // launch animation
            animator = other.gameObject.GetComponent<Animator>();
            animator.SetTrigger("Stumble");

            // reload
            StartCoroutine(waitAndReload());
        }
    }

    IEnumerator waitAndReload()
    {
        yield return new WaitForSeconds(3f);
        Application.LoadLevel("scene_3");
    }

}
