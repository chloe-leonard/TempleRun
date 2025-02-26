using UnityEngine;
using TMPro;

public class TimeCount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    private float elapsedTime;
    private bool isRunning = true;

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    // Méthode pour obtenir le temps écoulé
    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    // Méthode pour réinitialiser le timer
    public void ResetTimer()
    {
        elapsedTime = 0f;
    }

    // Méthode pour arrêter/démarrer le timer sans désactiver le script
    public void SetRunning(bool running)
    {
        isRunning = running;
    }
}
