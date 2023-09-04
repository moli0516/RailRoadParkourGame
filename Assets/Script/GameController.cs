using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [Header("Timer Setting")]
    [SerializeField] private float timer = 180f;
    [SerializeField] private TextMeshProUGUI timerText;



    private int coin = 0;
    [SerializeField] private TextMeshProUGUI coinText;
    public int Coin { get => coin; set => coin = value; }
    public float Timer { get => timer; set => timer = value; }

    public UnityEvent OnWin;
    public UnityEvent OnFail;
    private void Update()
    {
        if (timer < 0)
        {
            timer = 0;
            coinText.text = Coin.ToString();
            OnWin?.Invoke();
            return;
        }
        timer -= Time.deltaTime;
        DisplayTime(timer);
    }
    private void DisplayTime(float displayTime)
    {
        if (displayTime < 0) displayTime = 0;
        float mins = Mathf.FloorToInt(displayTime / 60);
        float secs = Mathf.FloorToInt(displayTime % 60);
        float msecs = displayTime % 1 * 1000;
        string timerTxt = string.Format("{0:00}:{1:00}:{2:000}", mins, secs, msecs);
        timerText.text = timerTxt;
    }
}
