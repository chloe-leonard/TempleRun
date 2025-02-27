using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // 🎯 Référence au personnage
    private float fixedY;    // Hauteur fixe du plane

    private Animator animator;
    private PlayerMovement playerMovement;
    private TimeCount timer;

    void Start()
    {
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
            // Stop timer
            timer = FindFirstObjectByType<TimeCount>();

            float finalTime = timer.GetElapsedTime();
            timer.SetRunning(false);

            Debug.Log("Temps final: " + finalTime);

            // launch animation
            animator = other.gameObject.GetComponent<Animator>();
            animator.SetTrigger("Fall");



            // reload
            StartCoroutine(waitAndReload());

        }

    }

    IEnumerator waitAndReload()
    {
        yield return new WaitForSeconds(3f);
        Application.LoadLevel("scene_anais3");
    }
}
