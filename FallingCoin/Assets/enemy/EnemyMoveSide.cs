using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveSide : MonoBehaviour
{
    private Vector2 initialPosition;
    void Start()
    {
        // 最初のオブジェクトの座標を取得する
        initialPosition = transform.position;
    }

    void FixedUpdate()
    {
        // 左右の動きの大きさを計算する
        transform.position = new Vector2(Mathf.Sin(Time.time) * 5.0f + initialPosition.x, initialPosition.y);
    }
}
