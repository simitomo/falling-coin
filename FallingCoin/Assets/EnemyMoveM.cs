using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveM : MonoBehaviour
{
    float moveX = 0.1f;
    float moveY = 0.1f;

    Vector2 initialPosition;

    const float kLimitX = 10f;
    const float kLimitY = 5f;
    const float k_LimitX = -10.3f;
    const float k_LimitY = 0f;

    void Start()
    {
        // 最初の位置の取得
        initialPosition = transform.position;
    }

    void FixedUpdate()
    {
        initialPosition.x += moveX;
        initialPosition.y += moveY;

        if (kLimitX <= initialPosition.x)       // オブジェクトがXの限界値に来た時
        {
            moveX *= -1;                        // 反転して反対方向に移動させる
                                    //            initialPosition.x = kLimitX;
        }
        else if (kLimitY <= initialPosition.y)  // オブジェクトがYの限界値に来た時
        {
            moveY *= -1;                        // 反転して反対方向に移動させる
                                    //            initialPosition.y = kLimitY;
        }
        else if (k_LimitX >= initialPosition.x)
        {
            moveX *= -1;
            //            initialPosition.x = -kLimitX;
        }
        else if (k_LimitY >= initialPosition.y)
        {
            moveY *= -1;
            //            initialPosition.y = 0;
        }

        transform.position = new Vector2(initialPosition.x, initialPosition.y);
    }
}