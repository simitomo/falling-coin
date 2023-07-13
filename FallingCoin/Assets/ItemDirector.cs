using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDirector : MonoBehaviour
{
    PlayerBuff playerBuff;  // PlayerBuffスクリプトを呼び出す
    GameObject obj = GameObject.Find("ItemCounter");
    int speedupTurn;
    int invencebleUseNum;  

    void Start()
    {
        playerBuff = obj.GetComponent<PlayerBuff>();
        speedupTurn = playerBuff.GetSpeedupTurn();          // PlayerBuffで関数を呼び出し変数を取得する
        invencebleUseNum = playerBuff.GetInvincibleTurn();  // PlayerBuffで関数を呼び出し変数を取得する
    }

    void FixedUpdate()
    {
        if (speedupTurn >= 3)
        {

        }
    }
}
