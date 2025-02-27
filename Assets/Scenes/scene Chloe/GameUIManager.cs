using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public GameObject resultsPanel; // 🎯 Le panel des résultats
    public GameObject FirstPanel; // 🎯 Le panel de game over
    public TextMeshProUGUI scoreText, timeText; // 📝 Les textes à mettre à jour

    private void Start()
    {
        resultsPanel.SetActive(false); // 🔹 Cache le panneau au début
    }

    public void ShowResults(int score, float time)
    {
    Debug.Log($"📢 ShowResults() appelé avec Score: {score}, Temps: {time}");

    if (resultsPanel != null)
        {
            resultsPanel.SetActive(true);
            FirstPanel.SetActive(false);
            Debug.Log("✅ resultPanel activé !");
        }
        else
        {
            Debug.LogError("❌ resultPanel est NULL !");
        }
        scoreText.text = "Vous avez obtenu : " + score + " points";
        timeText.text = "Temps: " + time.ToString("F2") + "s"; // 🔹 Affiche 2 décimales
    }

    

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 🔄 Recharge la scène
    }
}
