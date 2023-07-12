using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TimerDirector : MonoBehaviour
{
    float countTime= 0;
    int countMin = 0;
    float countSec = 0;
   
    TextMeshProUGUI textTime;
    // Start is called before the first frame update
    void Start()
    {
        this.textTime = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

        countTime += Time.deltaTime; //Time.deltaTimeでカウントした秒数をcountTimeに格納

        countMin = (int)(countTime / 60);
        countSec = countTime % 60;
        
        this.textTime.SetText("{0:0}:{1:2}", countMin, countSec);
       // GetComponent<Text>().text= countTime.ToString("F2");//少数2桁まで表示
    }
}
