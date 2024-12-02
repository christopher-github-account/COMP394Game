using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    public TMP_Text textTimer;

    private float timer = 0.0f;
    private bool isTimer = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isTimer = true;
    }

    void DisplayTime()
    {
        int minutes = Mathf.FloorToInt(timer / 60.0f);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        textTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void Update()
    {
        if(isTimer)
        {
            timer += Time.deltaTime;
            DisplayTime();
        }
        
    }
}
