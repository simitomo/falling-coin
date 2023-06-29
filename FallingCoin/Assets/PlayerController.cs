using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// マウスを押した地点から離した地点までの座標を計算して返す
    private Vector2 PlayerPos(Vector2 startPos, Vector2 endPos)
    {
        Vector2 temp;
        // それぞれの軸に加える力を調節する
        temp.x = (startPos.x - endPos.x) / 1;
        temp.y = (startPos.y - endPos.y) * 2;
        return temp;
    }

    // タグ検索用で定数を用意
    const string kEnemyTag = "enemy";

    // マウスを押した地点の座標を入れる変数
    Vector2 startPos = new Vector2();

    // 強めに重力をかけるための変数
    Vector2 gravity = new Vector2(0, -9.8f * 4);

    // ジャンプをするための関数
    Vector2 jumpForce = new Vector2(0, 500f);

    // AddForceを使う用の変数
    Rigidbody2D rigid;
    // バフを使う用の変数
    PlayerBuff buff;

    void Start()
    {
        // Rigidbody2Dの機能(AddForce)を使えるように参照する
        this.rigid = GetComponent<Rigidbody2D>();
        // PlayerBuffのスクリプトを参照できるようにする
        buff = GetComponent<PlayerBuff>();
    }

    void Update()
    {
        // マウスが押された地点を代入
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }

        // マウスが離された地点を代入し押した地点からの座標を求める
        // 求めた座標分だけ力を加える
        if (Input.GetMouseButtonUp(0))
        {
            // subPosにマウスを押した地点から離した地点を引いた座標を入れる
            Vector2 playerPos = PlayerPos(startPos, Input.mousePosition);

            // 以下のスピード以内の時動作する
            //if (-10 < this.rigid.velocity.x && this.rigid.velocity.x < 10)
            {
                playerPos = buff.PlayerSpeedup(playerPos);
                // 引っ張った距離だけ力を加える
                this.rigid.AddForce(playerPos);
            }

        }

        // 縦軸に動いていない場合かつスペースキーが押された場合ジャンプする
        if (Input.GetKeyDown(KeyCode.Space))//(Input.GetKeyDown(KeyCode.Space) && this.rigid.velocity.y == 0)
        {
            //this.rigid.AddForce(jumpForce);
            this.rigid.velocity = new Vector2(-this.rigid.velocity.x, this.rigid.velocity.y);
        }

        if (Input.GetKey(KeyCode.F))
        {
            this.rigid.velocity = new Vector2();
        }
    }

    void FixedUpdate()
    {
        // 縦軸のスピードが0じゃないとき重力を強めにかける
        if (this.rigid.velocity.y != 0)
        {
            Debug.Log("落下");
            this.rigid.AddForce(gravity);
        }
    }

    // 敵に触れた場合動作する
    void OnTriggerEnter2D(Collider2D collision)
    {
        // "enemy"というタグがつけられたオブジェクトのみ動作するようにする
        if (collision.gameObject.CompareTag(kEnemyTag))
        {
            // プレイヤーに無敵がついていない場合　　縦軸の速度はそのままで横軸のスピードを1/10にする
            if (buff.isPlayerInvincible())
            {
                this.rigid.velocity = new Vector2(this.rigid.velocity.x / 10, this.rigid.velocity.y);
            }

            // 触れた敵を消す
            Destroy(collision.gameObject);
        }
    }
}
