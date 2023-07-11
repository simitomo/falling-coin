using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerDirector : MonoBehaviour
{
    float countTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        countTime += Time.deltaTime; //Time.deltaTimeでカウントした秒数をcountTimeに格納

        GetComponent<Text>().text= countTime.ToString("F2");//少数2桁まで表示
    }
}
