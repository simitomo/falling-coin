using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBuff : MonoBehaviour
{
    public GameObject canvas;
    Camera cam;
    Vector2 SetPos;

    AudioSource aud;
    [SerializeField] private AudioClip[] clips;
    public AudioClip getSpeedUpSe;
    public AudioClip getGaurdSe;

    int audioNo = 0;
    float alphaBgm = 1.0f;
    float alphaSpeed = 0.0625f;
    bool isFadeBgm = false;
    bool isChangBgm = false;

    // スピードアップが適用するターン数
    const int kSpeedupTurnMax = 3;
    [SerializeField] GameObject[] speedUp;
    GameObject speedUpInstance = null;

    // buffでスピードアップする力
    const float kSpeedupPower = 2;

    // 使用可能ターン用変数
    int speedupTurn;

    // 無敵が適用される回数
    const int kInvincibleUseNumMax = 3;
    [SerializeField] GameObject[] invincible;
    GameObject invincibleInstance = null;

    // 無敵が適用される回数用変数
    int invincibleUseNum;


    void Start()
    {
        cam = Camera.main;

        aud = GetComponent<AudioSource>();
        aud.clip = clips[audioNo];
        aud.Play();

        // ターン数の初期化
        speedupTurn = 0;
        invincibleUseNum = 0;
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

        if (speedUpInstance != null)
        {
            SetPos = cam.WorldToScreenPoint(this.gameObject.transform.position);
            SetPos.x += 10f;
            SetPos.y += 10f;
            speedUpInstance.transform.position = SetPos;
        }

        if (invincibleInstance != null)
        {
            SetPos = cam.WorldToScreenPoint(this.gameObject.transform.position);
            SetPos.x -= 10f;
            SetPos.y += 10f;
            invincibleInstance.transform.position = SetPos;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // スピードアップアイテムを拾ったとき
        if (collision.gameObject.CompareTag("speedup"))
        {
            // ターンの増加
            speedupTurn = kSpeedupTurnMax;

            this.aud.PlayOneShot(this.getSpeedUpSe);

            if (speedUpInstance != null) Destroy(speedUpInstance);

            speedUpInstance = Instantiate(speedUp[speedupTurn - 1]);
            speedUpInstance.transform.SetParent(canvas.transform, false);

            SetPos = cam.WorldToScreenPoint(this.gameObject.transform.position);
            SetPos.x += 10f;
            SetPos.y += 10f;
            speedUpInstance.transform.position = SetPos;

            // 拾ったアイテムの削除
            Destroy(collision.gameObject);
        }

        // 無敵アイテムを拾ったとき
        if (collision.gameObject.CompareTag("invincible"))
        {
            // ターンの増加
            invincibleUseNum = kInvincibleUseNumMax;

            this.aud.PlayOneShot(this.getGaurdSe);
            if (invincibleInstance != null) Destroy(invincibleInstance);

            invincibleInstance = Instantiate(invincible[invincibleUseNum - 1]);
            invincibleInstance.transform.SetParent(canvas.transform, false);

            SetPos = cam.WorldToScreenPoint(this.gameObject.transform.position);
            SetPos.x -= 10f;
            SetPos.y += 10f;
            invincibleInstance.transform.position = SetPos;

            // 拾ったアイテムの削除
            Destroy(collision.gameObject);
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
                Destroy(speedUpInstance);

                speedUpInstance = Instantiate(speedUp[speedupTurn - 1]);
                speedUpInstance.transform.SetParent(canvas.transform, false);

            }
            else
            {
                if (speedUpInstance != null)    Destroy(speedUpInstance);
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
                Destroy(invincibleInstance);

                invincibleInstance = Instantiate(invincible[invincibleUseNum - 1]);
                invincibleInstance.transform.SetParent(canvas.transform, false);
            }
            else
            {
                if (invincibleInstance != null) Destroy(invincibleInstance);
            }

            // 受けないとして返す
            return false;
        }

        // 受けるとして返す
        return true;
    }


    void Update()
    {
#if UNITY_EDITOR
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
#endif
    }
}
