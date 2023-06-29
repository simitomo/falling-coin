using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBuff : MonoBehaviour
{
    public Vector2 PlayerSpeedup(Vector2 pos)
    {
        Vector2 temp = pos;

        if (speedupTurn > 0)
        {
            temp.x *= kSpeedupPower;
            //speedupTurn--;
        }

        return temp;
    }

    public bool isPlayerInvincible()
    {
        // 無敵がついている場合
        if (invincibleUseNum > 0)
        {
            invincibleUseNum--;
            return false;
        }

        return true;
    }

    // タグ検索用
    const string kSpeedupTag = "speedup";
    const string kInvincibleTag = "invincible";

    // スピードアップが適用するターン数
    const int kSpeedupTurnMax = 3;

    // buffでスピードアップする力
    const float kSpeedupPower = 3;

    // 使用可能ターン用変数
    int speedupTurn;

    // 無敵が適用される回数
    const int kInvincibleUseNumMax = 3;

    // 無敵が適用される回数用変数
    int invincibleUseNum;

    void Start()
    {
        // ターン数の初期化
        speedupTurn = 0;
        invincibleUseNum = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // スピードアップアイテムを拾ったとき
        if (collision.gameObject.CompareTag(kSpeedupTag))
        {
            // ターンの増加
            speedupTurn = kSpeedupTurnMax;

            // 拾ったアイテムの削除
            Destroy(collision.gameObject);
        }

        // 無敵アイテムを拾ったとき
        if (collision.gameObject.CompareTag(kInvincibleTag))
        {
            // ターンの増加
            invincibleUseNum = kInvincibleUseNumMax;

            // 拾ったアイテムの削除
            Destroy(collision.gameObject);
        }
    }
}
