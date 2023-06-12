using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// マウスを押した地点から離した地点までの座標の計算
    private Vector2 AddPos(Vector2 startPos, Vector2 endPos)
    {
        Vector2 temp;
        temp.x = startPos.x + endPos.y;
        temp.y = startPos.y + endPos.y;
        return temp;
    }

    // マウスを押した座標を入れる変数
    Vector2 startPos = new Vector2();

    // Rigidbody2D型の変数を用意
    Rigidbody2D rigid;

    void Start()
    {
        // rigidbodyを扱えるように機能を持ってくる
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // マウスが押された地点の座標を代入
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("U_OK");
            startPos = Input.mousePosition;
        }

        // マウスが離された座標を代入
        // 引っ張った量だけ力を加える
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 addPos = AddPos(startPos, Input.mousePosition);

            this.rigid.AddForce(addPos);
        }
    }

    void FixedUpdate()
    {
        // 地面についていない場合重力を通常の2倍かける
        if (this.rigid.velocity.y != 0)
        {
            Debug.Log("FU_OK");
            this.rigid.AddForce(transform.up * -19.6f);
        }
    }
}
