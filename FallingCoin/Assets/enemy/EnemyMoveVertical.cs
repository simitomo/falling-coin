using UnityEngine;
using System.Collections;

public class EnemyMoveVertical : MonoBehaviour
{
    private Vector2 initialPosition;

    void Start()
    {
        // 最初のオブジェクトの座標を取得する
        initialPosition = transform.position;
    }

    void Update()
    {
        // 上下の動きの大きさを計算する
        transform.position = new Vector2(initialPosition.x,Mathf.Sin(Time.time)*10.0f+initialPosition.y);
    }
}
