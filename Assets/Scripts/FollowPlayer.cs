using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    TimeCount timer;
    GameUIManager uiManager;

    public Transform player; // 🎯 Référence au personnage
    private float fixedY;    // Hauteur fixe du plane

    private Animator animator;

    ScoreManager scoreManager;

    void Start()
    {
        scoreManager = GameObject.Find("Canvas").GetComponent<ScoreManager>();

        // Récupérer correctement le script TimeCount dans la scène
        timer = FindFirstObjectByType<TimeCount>();  // Trouve le script TimeCount dans la scène
        if (timer == null)
        {
            Debug.LogError("TimeCount script not found in the scene!");
        }

        // Récupérer correctement le script GameUIManager dans la scène
        uiManager = FindFirstObjectByType<GameUIManager>();  // Trouve le script GameUIManager dans la scène
        if (uiManager == null)
        {
            Debug.LogError("GameUIManager script not found in the scene!");
        }

        // Stocker la hauteur initiale du plane
        fixedY = transform.position.y;
    }


    void Update()
    {

        // Suivre le joueur en X et Z, mais garder Y fixe
        transform.position = new Vector3(player.position.x, fixedY, player.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator = other.gameObject.GetComponent<Animator>();

            // launch animation
            if (animator != null)
            {
                animator.SetTrigger("Fall");
            }
            else
            {
                Debug.LogError("❌ Animator component not found on the player!");
            }

            if (timer != null) // Vérifie que timer n'est pas null avant d'y accéder
            {
                timer.SetRunning(false);
                float finalTime = timer.GetElapsedTime();
                StartCoroutine(waitAndReload(scoreManager.GetScore(), finalTime));
            }
            else
            {
                Debug.LogError("TimeCount script not found in the scene!");
            }
        }
    }

    IEnumerator waitAndReload(int score, float finalTime)
    {
        yield return new WaitForSeconds(3f);
        uiManager.ShowResults(score, finalTime);
    }
}
