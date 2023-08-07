using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveSlanting : MonoBehaviour
{
    Vector2 initialPosition;
    void Start()
    {
        // 最初のオブジェクトの座標を取得する
        initialPosition = transform.position;
    }

    void FixedUpdate()
    {
        transform.position = new Vector2(Mathf.Sin(Time.time)*1.5f+initialPosition.x, Mathf.Sin(Time.time) * 3.0f + initialPosition.y);
    }
}
