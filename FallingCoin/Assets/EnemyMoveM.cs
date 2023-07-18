using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveM : MonoBehaviour
{
    // 1fごとの移動量
    float moveX = 0.1f;
    float moveY = 0.1f;

    // 最初の位置を代入するための変数
    Vector2 initialPosition;

    // X軸とY軸の正の方向に進める限界値
    const float kLimitX = 10f;
    const float kLimitY = 5f;
    // X軸とY軸の負の方向に進める限界値
    const float k_LimitX = -10.3f;
    const float k_LimitY = 0f;

    void Start()
    {
        // 最初の位置の取得
        initialPosition = transform.position;
    }

    void FixedUpdate()
    {
        // 最初の位置に移動する量を足す
        initialPosition.x += moveX;
        initialPosition.y += moveY;

        if (kLimitX <= initialPosition.x)       // オブジェクトがXの正の方向の限界値に来た時
        {
            moveX *= -1;                        // 反転して負の方向に移動させる
        }
        else if (kLimitY <= initialPosition.y)  // オブジェクトがYの正の方向の限界値に来た時
        {
            moveY *= -1;                        // 反転して負の方向に移動させる
        }
        else if (k_LimitX >= initialPosition.x) // オブジェクトがXの負の方向の限界値に来た時
        {
            moveX *= -1;                        // 反転して正の方向に移動させる
        }
        else if (k_LimitY >= initialPosition.y) // オブジェクトがYの負の方向の限界値に来た時
        {
            moveY *= -1;                        // 反転して正の方向に移動させる
        }

        // オブジェクトを動かす
        transform.position = new Vector2(initialPosition.x, initialPosition.y);
    }
}