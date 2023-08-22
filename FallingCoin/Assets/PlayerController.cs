using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // 音声を流すよう
    AudioSource aud;

    // 音声ファイル入れる用
    public AudioClip coinSnapSE;
    public AudioClip windoNoiseSE;
    public AudioClip HitSe;
    public AudioClip GuardSE;
    public AudioClip getGemSe;

    // パーティクル用
    ParticleSystem par;

    // ロード用
    LoadScene scene;
    TimeDirector time;

    // 点滅用
    SpriteRenderer sprite;
    bool isFlash = false;
    float flashAlpha = 1.0f;
    int flashFrame = 0;
    Color c = new Color(1, 1, 1);

    // マウスを押した地点の座標を入れる変数
    Vector2 startPos = new Vector2();

    // 強めに重力をかけるための変数
    Vector2 gravity = new Vector2(0, -19.6f);

    // プレイヤーにパワーを追加
    Vector2 playerPos;

    // AddForceを使う用の変数
    Rigidbody2D rigid;
    // バフを使う用の変数
    PlayerBuff buff;
    // スコアアップ用の変数
    Score score;

    // 力を加えるかのフラグ
    bool isPower = false;

    Vector3 arrow = new Vector3(0, 0, 1.0f);

    void Start()
    {
        // AudioSourceを使えるように
        aud = GetComponent<AudioSource>();
        // ParticleSystemを使えるように
        par = GetComponent<ParticleSystem>();
        // パーティクルを止める
        par.Stop();
        // シーンのロード用
        scene = GetComponent<LoadScene>();
        time = GameObject.Find("Timer").GetComponent<TimeDirector>();
        // 点滅用
        sprite = GetComponent<SpriteRenderer>();
        // Rigidbody2Dの機能(AddForce)を使えるように参照する
        this.rigid = GetComponent<Rigidbody2D>();
        // PlayerBuffのスクリプトを参照できるようにする
        buff = GetComponent<PlayerBuff>();
        // ScoreDirectorのスクリプトを参照できるようにする
        score = GameObject.Find("Score").GetComponent<Score>();
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
            playerPos = PlayerPos(startPos, Input.mousePosition);
            // 速度アップ処理
            playerPos = buff.PlayerSpeedup(playerPos);

            isPower = true;
        }

        // 右クリックが押された場合にX軸方向のスピードを反転する
        if (Input.GetMouseButtonDown(1))
        {
            this.rigid.velocity = new Vector2(-this.rigid.velocity.x, this.rigid.velocity.y);

            // はじいた時の音を鳴らす
            this.aud.PlayOneShot(this.windoNoiseSE);
        }

#if UNITY_EDITOR
        // デバック用コード
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // 速度0(停止)
            if (Input.GetKey(KeyCode.Space))
            {
                this.rigid.velocity = Vector2.zero;
            }
        }
#endif
    }

    void FixedUpdate()
    {
        this.transform.rotation *= Quaternion.AngleAxis(-this.rigid.velocity.x, arrow);

        // 縦軸のスピードが0じゃないとき重力を強めにかける
        if (this.rigid.velocity.y != 0)
        {
            this.rigid.AddForce(gravity);
        }

        if (isPower)
        {
            // 引っ張った距離だけ力を加える
            this.rigid.AddForce(playerPos, ForceMode2D.Impulse);

            // はじいた時の音を鳴らす
            this.aud.PlayOneShot(this.coinSnapSE);

            isPower = false;

            // パーティクルスタート
            par.Play();
        }

        if (-5f <= this.rigid.velocity.x 
            && this.rigid.velocity.x <= 5f
            && this.rigid.velocity.y == 0)
        {
                // パーティクル停止
                par.Stop();
        }

        if (isFlash)
        {
            flashFrame++;

            if (flashFrame % 10 == 0)
            {
                if (flashAlpha == 1.0f) flashAlpha = 0.0f;
                else                    flashAlpha = 1.0f;
            }

            if (50 <= flashFrame)
            {
                isFlash = false;
                flashAlpha = 1.0f;
            }

            c.a = flashAlpha;

            sprite.color = c;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("underFloor"))
        {
            score.SetScore();

            time.TimeSet();

            scene.LoadMap();
        }
    }

    // 敵に触れた場合動作する
    void OnTriggerEnter2D(Collider2D collision)
    {
        // "enemy"というタグがつけられたオブジェクトのみ動作するようにする
        if (collision.gameObject.CompareTag("enemy"))
        {
            // プレイヤーに無敵がついていない場合
            // プレイヤーを点滅
            // 速度ダウン、スコアダウン
            if (buff.isPlayerInvincible())
            {
                isFlash = true;
                flashFrame = 0;

                this.rigid.velocity = new Vector2(this.rigid.velocity.x / 16, this.rigid.velocity.y / 32);
                score.ScoreDonw();

                this.aud.PlayOneShot(this.HitSe);
            }
            else
            {
                this.aud.PlayOneShot(this.GuardSE);
            }

            // 触れた敵を消す
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("score"))
        {
            // スコアをアップする
            score.ScoreUp();

            this.aud.PlayOneShot(this.getGemSe);

            // 触れたアイテムを消す
            Destroy(collision.gameObject);
        }
    }

    /// マウスを押した地点から離した地点までの座標を計算して返す
    private Vector2 PlayerPos(Vector2 startPos, Vector2 endPos)
    {
        Vector2 temp;
        // それぞれの軸に加える力を調節する
        temp.x = (startPos.x - endPos.x) / 24;
        temp.y = (startPos.y - endPos.y) / 16;
        return temp;
    }
}
