using UnityEngine;

public class OnTriggerEnterScript : MonoBehaviour
{
    ScoreManager scoreManager;
    
    void Start () {
        scoreManager = GameObject.Find("Canvas").GetComponent<ScoreManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scoreManager.IncreaseScore();
            gameObject.SetActive(false);
        }
    }
}
