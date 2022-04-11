using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerController : MonoBehaviour
{
	
    [SerializeField] private TextMeshProUGUI timerText;
	
    public float StartTime = 10;
    public float time;
    private bool isTimerRunnig = false;

    private void Start()
    {
		time = StartTime;
        timerText.text = time.ToString();
    }

    private void Update()
    {
        if (isTimerRunnig == true)
        {
            if (time < 0)
            {
                StopTimer();
            }

            time -= Time.deltaTime;
            timerText.text = Mathf.Round(time).ToString();
        }
    }

    public void StartTimer()
    {
        isTimerRunnig = true;
    }

    public void ResetTimer()
    {
        isTimerRunnig = false;
        time = StartTime;
		timerText.text = Mathf.Round(time).ToString();
    }
    public void StopTimer()
    {
        isTimerRunnig = false;
    }
}
