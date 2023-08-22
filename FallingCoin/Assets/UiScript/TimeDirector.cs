using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDirector : MonoBehaviour
{
    int frameCount;
    int miliSec;
    int sec;
    int min;

    Text textTimer;

    // Start is called before the first frame update
    void Start()
    {
        this.textTimer = GetComponent<Text>();
        frameCount = PlayerPrefs.GetInt("Time", 0);

        Debug.Log("[Time]Start : " + frameCount);
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

        this.textTimer.text = min.ToString() + ":" + sec.ToString("D2")+"."+miliSec.ToString("D2");
    }

    public void TimeSet()
    {
        PlayerPrefs.SetInt("Time", frameCount);

        Debug.Log("[Time]End : " + frameCount);
    }

    void Update()
    {
#if UNITY_EDITOR
        //デバック用コード
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.T))
            {
                frameCount++;
            }
        }
#endif
    }
}
