using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuff : MonoBehaviour
{
    public Vector2 PlayerpeedUp(Vector2 pos)
    {
        Vector2 temp = pos;
        if (speedupTurn > 0)
        {
            temp.x *= SPEEDUP_POWER;
            speedupTurn--;
        }
        return temp;
    }

    // タグ検索用
    const string SPEEDUP_TAG = "speedup";

    // buffが適用するターン数
    const int TURN_MAX_SPEEDUP = 3;

    // buffでアップする力
    const float SPEEDUP_POWER = 5;

    // 使用可能ターン用変数
    int speedupTurn;

    void Start()
    {
        // ターン数の初期化
        speedupTurn = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(SPEEDUP_TAG))
        {
            // ターンの増加
            speedupTurn = TURN_MAX_SPEEDUP;

            // 拾ったアイテムの削除
            Destroy(collision.gameObject);
        }
    }
}
