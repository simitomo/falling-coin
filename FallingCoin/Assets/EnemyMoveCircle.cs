using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveCircle : MonoBehaviour
{
    // 円の半径を設定します
    public float radius = 0.5f;

    // 初期位置を取得し、高さを保持する
    Vector3 initPos;

    void Start()
    {
        // 初期位置を保持する
        initPos = gameObject.transform.position;
    }

    void FixedUpdate()
    {
        // オブジェクトの位置を計算する

        // 位相を計算する
        float phase = Time.time * Mathf.PI;

        // 現在の位置を計算する
        float xPos = radius * Mathf.Cos(phase);
        float yPos = radius * Mathf.Sin(phase);

        // ゲームオブジェクトの位置を設定します。
        Vector2 pos = new Vector2(xPos, yPos);
        gameObject.transform.position = pos;
    }
}
