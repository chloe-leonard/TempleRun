using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    public TextMeshProUGUI TimerText { get => timerText; set => timerText = value; }


    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0) {
            remainingTime -= Time.deltaTime;
        } else if (remainingTime < 0) {
            remainingTime = 0;
            TimerText.text = "Time's Up!";
            TimerText.color = Color.red;
        }
        int minutes = Mathf.FloorToInt(remainingTime /60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
    }
}
