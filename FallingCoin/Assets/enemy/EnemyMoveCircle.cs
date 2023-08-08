using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveCircle : MonoBehaviour
{
    // 円の半径を設定します
    const float kRadius = 0.1f;

    float xPos;
    float yPos;

    // 初期位置を取得し、高さを保持する
    Vector2 initPos;

    void Start()
    {
        // 初期位置を保持する
        initPos = gameObject.transform.position;
    }

    void FixedUpdate()
    {
        initPos.x += xPos;
        initPos.y += yPos;
        // オブジェクトの位置を計算する

        // 位相を計算する
        float phase = Time.time * Mathf.PI;

        // 現在の位置を計算する
        xPos = kRadius * Mathf.Cos(phase);
        yPos = kRadius * Mathf.Sin(phase);

        // ゲームオブジェクトの位置を設定します。
        Vector2 pos = new Vector2(initPos.x, initPos.y);
        gameObject.transform.position = pos;
    }
}
