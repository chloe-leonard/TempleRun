using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    int score;

    void Start()
    {
        scoreText.text = "Score: " +score;
    }


    public void IncreaseScore() {
        score ++;        
        scoreText.text = "Score: " +score;

        }

    // ✅ Fonction pour récupérer le score actuel
    public int GetScore() 
    {
        return score;
    }
}
