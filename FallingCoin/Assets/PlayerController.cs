using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 横軸の力を強化時と通常時で変えるよう
    int ShotPowerX(int count)
    {
        if (count > 0)
        {
            return 2;
        }
        return 1;
    }

    // 強化アイテムの残りターンによるプレイヤーのパワーアップをOnにするかOffにするか
    bool BuffCheck(int count)
    {
        if (count > 0)
        {
            return true;
        }
        return false;
    }

    /// マウスを押した地点から離した地点までの座標を計算して返す
    private Vector2 SubPos(Vector2 startPos, Vector2 endPos, int powerX)
    {
        Vector2 temp;
        // それぞれの軸に加える力を調節する
        temp.x = (startPos.x - endPos.x) / 2 * powerX;
        temp.y = (startPos.y - endPos.y) * 2;
        return temp;
    }

    // タグ検索用で定数を用意
    const string ENEMY_TAG = "enemy";
    const string SPEEDUP_TAG = "speedup";
    const string ENEMY_KILL_TAG = "enemyKill";
    const string WALL_BREAK_TAG = "wallBreak";

    // 強化アイテムを使える回数を定数
    const int BUFF_SPEEDUP_NUM = 3;
    const int BUFF_ENEMY_KILL_NUM = 3;
    const int BUFF_WALL_BREAK_NUM = 3;

    // 強化アイテムを残り何ターン使えるかを入れる変数
    int buffSpeedUpTurn;
    int buffEnemyKillTurn;
    int buffWallBreakTurn;

    // 敵を倒すことができるかの判定
    bool buffEnemyKill;

    // 特定の壁を壊すことができるかの判定
    bool buffWallBreak;

    // マウスを押した地点の座標を入れる変数
    Vector2 startPos = new Vector2();

    // 強めに重力をかけるための変数
    Vector2 gravity = new Vector2(0, -19.6f);

    // ジャンプをするための関数
    Vector2 jumpForce = new Vector2(0, 500f);

    // AddForceを使う用の変数
    Rigidbody2D rigid;
        
    void Start()
    {
        // Rigidbody2Dの機能(AddForce)を使えるように参照する
        rigid = GetComponent<Rigidbody2D>();

        // 強化アイテムの使えるターンの初期化
        buffSpeedUpTurn = 0;
        buffEnemyKillTurn = 0;
        buffWallBreakTurn = 0;

        // 強化アイテムの判定の初期化
        buffEnemyKill = false;
        buffWallBreak = false;
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
            Vector2 subPos = SubPos(startPos, Input.mousePosition, ShotPowerX(buffSpeedUpTurn));

            // 以下のスピード以内の時動作する
            if (-10 < this.rigid.velocity.x && this.rigid.velocity.x < 10)
            {
                // 引っ張った距離だけ力を加える
                this.rigid.AddForce(subPos);

                Debug.Log(buffEnemyKillTurn);

                // 強化アイテムが使えるか
                buffEnemyKill = BuffCheck(buffEnemyKillTurn);
                buffWallBreak = BuffCheck(buffWallBreakTurn);

                // 強化ターンが残っている場合ターンを減らす
                if (buffSpeedUpTurn > 0)
                {
                    buffSpeedUpTurn--;
                }
                if (buffEnemyKillTurn > 0)
                {
                    buffEnemyKillTurn--;
                }
                if (buffWallBreakTurn > 0)
                {
                    buffWallBreakTurn--;
                }
            }

        }

        // 縦軸に動いていない場合かつスペースキーが押された場合ジャンプする
        if (Input.GetKeyDown(KeyCode.Space) && this.rigid.velocity.y == 0)
        {
            this.rigid.AddForce(jumpForce);
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
        if (collision.gameObject.CompareTag(ENEMY_TAG))
        {
            // 敵を倒せない場合スピードダウン
            if (buffEnemyKill == false)
            {
                // 縦軸の速度はそのままで横軸のスピードを1/10にする
                this.rigid.velocity = new Vector2(this.rigid.velocity.x / 10, this.rigid.velocity.y);
            }

            // 触れた敵を消す
            Destroy(collision.gameObject);
        }

        // "enemyKill"というタグがつけられたオブジェクトのみ動作するようにする
        if (collision.gameObject.CompareTag(ENEMY_KILL_TAG))
        {
            // enemyKillできるターンを最大まで増やす
            buffEnemyKillTurn = BUFF_ENEMY_KILL_NUM;

            // 触れたアイテムを消す
            Destroy(collision.gameObject);
        }

        //// "speedup"というタグがつけられたオブジェクトのみ動作するようにする
        //if (collision.gameObject.CompareTag(SPEEDUP_TAG))
        //{
        //    // speedupできるターンを最大まで増やす
        //    buffSpeedUpTurn = BUFF_SPEEDUP_NUM;

        //    // 触れたアイテムを消す
        //    Destroy(collision.gameObject);
        //}

        //// "wallBreak"というタグがつけられたオブジェクトのみ動作するようにする
        //if (collision.gameObject.CompareTag(WALL_BREAK_TAG))
        //{
        //    // wallBreakできるターンを最大まで増やす
        //    buffWallBreakTurn = BUFF_WALL_BREAK_NUM;

        //    // 触れたアイテムを消す
        //    Destroy(collision.gameObject);
        //}
    }
}
