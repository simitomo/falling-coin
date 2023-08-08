using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveM : MonoBehaviour
{
    // 1fごとの移動量
    float moveX = 0.125f;
    float moveY = 0.125f;

    // 最初の位置を代入するための変数
    Vector2 initPos;
    Vector2 pos;

    // X軸とY軸の正の方向に進める限界値
//    const float kLimitX = 8.28f;
    const float kLimitX = 8f;
    const float kLimitY = 4f;
    // X軸とY軸の負の方向に進める限界値
//    const float k_LimitX = -8.75f;
    const float k_LimitX = -8f;
    const float k_LimitY = 0f;

    void Start()
    {
        // 最初の位置の取得
        initPos = gameObject.transform.position;
        pos = initPos;
    }

    void FixedUpdate()
    {
        // 最初の位置に移動する量を足す
        pos.x += moveX;
        pos.y += moveY;

        if (kLimitX + initPos.x <= pos.x)       // オブジェクトがXの正の方向の限界値に来た時
        {
            pos.x = kLimitX + initPos.x;
            moveX *= -1;                        // 反転して負の方向に移動させる
        }
        else if (kLimitY + initPos.y <= pos.y)  // オブジェクトがYの正の方向の限界値に来た時
        {
            pos.y = kLimitY + initPos.y;
            moveY *= -1;                        // 反転して負の方向に移動させる
        }
        else if (k_LimitX + initPos.x  >= pos.x) // オブジェクトがXの負の方向の限界値に来た時
        {
            pos.x = k_LimitX + initPos.x;
            moveX *= -1;                        // 反転して正の方向に移動させる
        }
        else if (k_LimitY + initPos.y >= pos.y) // オブジェクトがYの負の方向の限界値に来た時
        {
            pos.y = k_LimitY + initPos.y;
            moveY *= -1;                        // 反転して正の方向に移動させる
        }

        // オブジェクトを動かす
        transform.position = new Vector2(pos.x, pos.y);
    }
}