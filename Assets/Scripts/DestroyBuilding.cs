using UnityEngine;

public class DestroyBuilding : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("building"))
        {
            Destroy(gameObject);
        }
    }
}
