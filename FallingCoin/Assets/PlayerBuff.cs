using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBuff : MonoBehaviour
{
    GameObject _countSpeedupText;
    GameObject _countInvincibleText;

    // タグ検索用
    const string kSpeedupTag = "speedup";
    const string kInvincibleTag = "invincible";

    // スピードアップが適用するターン数
    const int kSpeedupTurnMax = 3;

    // buffでスピードアップする力
    const float kSpeedupPower = 5;

    // 使用可能ターン用変数
    int speedupTurn;

    // 無敵が適用される回数
    const int kInvincibleUseNumMax = 3;

    // 無敵が適用される回数用変数
    int invincibleUseNum;


    void Start()
    {
        _countSpeedupText = GameObject.Find("SpeedupCounter");
        _countInvincibleText = GameObject.Find("InvincibleCounter");

        // ターン数の初期化
        speedupTurn = 0;
        invincibleUseNum = 0;
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
            _countSpeedupText.GetComponent<Text>().text = speedupTurn.ToString();
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
            _countInvincibleText.GetComponent<Text>().text = invincibleUseNum.ToString();

            // 受けないとして返す
            return false;
        }

        // 受けるとして返す
        return true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // スピードアップアイテムを拾ったとき
        if (collision.gameObject.CompareTag(kSpeedupTag))
        {
            // ターンの増加
            speedupTurn = kSpeedupTurnMax;

            // カウンターの増加
            _countSpeedupText.GetComponent<Text>().text = speedupTurn.ToString();

            // 拾ったアイテムの削除
            Destroy(collision.gameObject);
        }

        // 無敵アイテムを拾ったとき
        if (collision.gameObject.CompareTag(kInvincibleTag))
        {
            // ターンの増加
            invincibleUseNum = kInvincibleUseNumMax;

            // カウンターの増加
            _countInvincibleText.GetComponent <Text>().text = invincibleUseNum.ToString();

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
