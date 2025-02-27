using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    TimeCount timer;
    GameUIManager uiManager;

    public Transform player; // 🎯 Référence au personnage
    private float fixedY;    // Hauteur fixe du plane

    void Start()
    {
        scoreManager = GameObject.Find("Canvas").GetComponent<ScoreManager>();

        // Récupérer correctement le script TimeCount dans la scène
        timer = FindObjectOfType<TimeCount>();  // Trouve le script TimeCount dans la scène
        if (timer == null)
        {
            Debug.LogError("TimeCount script not found in the scene!");
        }

        // Récupérer correctement le script GameUIManager dans la scène
        uiManager = FindObjectOfType<GameUIManager>();  // Trouve le script GameUIManager dans la scène
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
        Application.LoadLevel("scene_3");
    }
}
