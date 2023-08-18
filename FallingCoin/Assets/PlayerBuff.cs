using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBuff : MonoBehaviour
{
    AudioSource aud;
    [SerializeField] private AudioClip[] clips;

    int audioNo = 0;
    float alphaBgm = 1.0f;
    float alphaSpeed = 0.0625f;
    bool isFadeBgm = false;
    bool isChangBgm = false;

    Text _countSpeedupText;
    Text _countInvincibleText;

    // スピードアップが適用するターン数
    const int kSpeedupTurnMax = 3;

    // buffでスピードアップする力
    const float kSpeedupPower = 2;

    // 使用可能ターン用変数
    int speedupTurn;

    // 無敵が適用される回数
    const int kInvincibleUseNumMax = 3;

    // 無敵が適用される回数用変数
    int invincibleUseNum;


    void Start()
    {
        aud = GetComponent<AudioSource>();
        aud.clip = clips[audioNo];
        aud.Play();

        _countSpeedupText = GameObject.Find("SpeedupCounter").GetComponent<Text>();
        _countInvincibleText = GameObject.Find("InvincibleCounter").GetComponent<Text>();

        // ターン数の初期化
        speedupTurn = 0;
        invincibleUseNum = 0;

        _countSpeedupText.text = "";
        _countInvincibleText.text = "";
    }

    void FixedUpdate()
    {
        if (audioNo == 0)
        {
            // 通常からアイテム使用BGMに
            if (0 < speedupTurn || 0 < invincibleUseNum)
            {
                audioNo = 1;
                isFadeBgm = true;
                //isChangBgm = true;
            }
        }
        if (audioNo == 1)
        {
            // アイテム使用から通常BGMに
            if (speedupTurn <= 0 && invincibleUseNum <= 0)
            {
                audioNo = 0;
                isFadeBgm = true;
                //isChangBgm = true;
            }
        }

        // フェードさせる
        if (isFadeBgm) FadeBgm();

        if (isChangBgm)
        {
            // 曲の変更
            aud.clip = clips[audioNo];
            // 曲の開始
            aud.Play();

            isChangBgm = false;
        }
    }

    void FadeBgm()
    {
        alphaBgm -= alphaSpeed;

        if (alphaBgm <= 0.0f)
        {
            alphaBgm = 0.0f;
            alphaSpeed *= -1;
            isChangBgm = true;
        }

        if (1.0f <= alphaBgm)
        {
            alphaBgm = 1.0f;
            alphaSpeed *= -1;
            isFadeBgm = false;
        }

        aud.volume = alphaBgm;
    }

    // プレイヤーの速度をアップ
    public Vector2 PlayerSpeedup(Vector2 pos)
    {
        Vector2 temp = pos;

        // スピードアップアイテムターンであれば
        if (speedupTurn > 0)
        {
            // 速度アップ
            temp.x *= kSpeedupPower;

            // ターンを減らす
            speedupTurn--;

            // カウンターの減少
            if (0 < speedupTurn)
            {
                _countSpeedupText.text = ("スピードアップ：" + speedupTurn.ToString());
            }
            else
            {
                _countSpeedupText.text = "";
            }
        }

        return temp;
    }

    // 敵の攻撃を受けるか
    public bool isPlayerInvincible()
    {
        // 無敵がついている場合
        if (invincibleUseNum > 0)
        {
            // ターンを減らす
            invincibleUseNum--;

            // カウンターの減少
            if (0 < invincibleUseNum)
            {
                _countInvincibleText.text = ("　　ガード　　：" + invincibleUseNum.ToString());
            }
            else
            {
                _countInvincibleText.text = "";
            }

            // 受けないとして返す
            return false;
        }

        // 受けるとして返す
        return true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // スピードアップアイテムを拾ったとき
        if (collision.gameObject.CompareTag("speedup"))
        {
            // ターンの増加
            speedupTurn = kSpeedupTurnMax;

            // カウンターの増加
            _countSpeedupText.text = ("スピードアップ：" + speedupTurn.ToString());

            // 拾ったアイテムの削除
            Destroy(collision.gameObject);
        }

        // 無敵アイテムを拾ったとき
        if (collision.gameObject.CompareTag("invincible"))
        {
            // ターンの増加
            invincibleUseNum = kInvincibleUseNumMax;

            // カウンターの増加
            _countInvincibleText.text = ("　　ガード　　：" + invincibleUseNum.ToString());

            // 拾ったアイテムの削除
            Destroy(collision.gameObject);
        }
    }

    void Update()
    {
        // デバッグ用コード
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // スピードアップアイテムターン追加
            if (Input.GetKeyDown(KeyCode.S))
            {
                speedupTurn = kSpeedupTurnMax;
            }
            // 無敵回数追加
            if (Input.GetKeyDown(KeyCode.D))
            {
                invincibleUseNum = kInvincibleUseNumMax;
            }
        }
    }
}
