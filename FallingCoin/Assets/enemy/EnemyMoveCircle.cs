using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveCircle : MonoBehaviour
{
    // 円の半径を設定
    const float kRadius = 0.125f;

    float xPos;
    float yPos;

    float phase;

    // 初期位置を取得し、高さを保持する
    Vector2 initPos;

    void Start()
    {
        // 初期位置を保持する
        initPos = gameObject.transform.position;
    }

    void FixedUpdate()
    {
        // オブジェクトの位置を計算する
        initPos.x += xPos;
        initPos.y += yPos;

        // 位相を計算する
        phase = Time.time * Mathf.PI * 0.5f;

        // 現在の位置を計算する
        xPos = kRadius * Mathf.Cos(phase);
        yPos = kRadius * Mathf.Sin(phase);

        // ゲームオブジェクトの位置を設定します。
        Vector2 pos = new Vector2(initPos.x, initPos.y);
        gameObject.transform.position = pos;
    }
}
