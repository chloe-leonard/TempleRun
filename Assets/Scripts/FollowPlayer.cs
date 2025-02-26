using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // 🎯 Référence au personnage
    private float fixedY;    // Hauteur fixe du plane

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
        Application.LoadLevel("scene_3");
    }
}
