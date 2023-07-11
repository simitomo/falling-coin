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
        temp.x = (startPos.x - endPos.x) / 2;
        temp.y = (startPos.y - endPos.y) * 2;
        return temp;
    }

    // タグ検索用で定数を用意
    const string kEnemyTag = "enemy";
    const string kScoreTag = "score";

    // マウスを押した地点の座標を入れる変数
    Vector2 startPos = new Vector2();

    // 強めに重力をかけるための変数
    Vector2 gravity = new Vector2(0, -19.6f);

    // ジャンプをするための関数
    Vector2 jumpForce = new Vector2(0, 500f);

    // AddForceを使う用の変数
    Rigidbody2D rigid;
    // バフを使う用の変数
    PlayerBuff buff;
    // スコアアップ用の変数
    ScoreDirector score;

    void Start()
    {
        // Rigidbody2Dの機能(AddForce)を使えるように参照する
        this.rigid = GetComponent<Rigidbody2D>();
        // PlayerBuffのスクリプトを参照できるようにする
        buff = GetComponent<PlayerBuff>();
        // ScoreDirectorのスクリプトを参照できるようにする
        score = GameObject.Find("Director").GetComponent<ScoreDirector>();
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
            // playerPosにマウスを押した地点から離した地点を引いた座標を入れる
            Vector2 playerPos = PlayerPos(startPos, Input.mousePosition);
            // 速度アップ処理
            playerPos = buff.PlayerSpeedup(playerPos);
            // 引っ張った距離だけ力を加える
            this.rigid.AddForce(playerPos);
        }

        // 右クリックが押された場合にX軸方向のスピードを反転する
        if (Input.GetMouseButtonDown(1))
        {
            this.rigid.velocity = new Vector2(-this.rigid.velocity.x, this.rigid.velocity.y);
        }

        // デバック用コード
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // 速度0(停止)
            if (Input.GetKey(KeyCode.Space))
            {
                this.rigid.velocity = Vector2.zero;
            }
        }
    }

    void FixedUpdate()
    {
        // 縦軸のスピードが0じゃないとき重力を強めにかける
        if (this.rigid.velocity.y != 0)
        {
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

        if (collision.gameObject.CompareTag(kScoreTag))
        {
            // スコアをアップする
            score.ScoreUp();

            // 触れたアイテムを消す
            Destroy(collision.gameObject);
        }
    }
}
