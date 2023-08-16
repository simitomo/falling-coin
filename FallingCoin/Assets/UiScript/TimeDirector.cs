using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeDirector : MonoBehaviour
{
    int frameCount;
    int miliSec;
    int sec;
    int min;

    TextMeshProUGUI textTimer;

    // Start is called before the first frame update
    void Start()
    {
        this.textTimer = GetComponent<TextMeshProUGUI>();
        frameCount = PlayerPrefs.GetInt("Time", 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        frameCount++;
        miliSec = frameCount * 100 / 50;
        sec = miliSec / 100;
        min = sec / 60;

        miliSec %= 100;
        sec %= 60;

        this.textTimer.SetText(min.ToString() + ":" + sec.ToString("D2")+"."+miliSec.ToString("D2"));
    }
}
