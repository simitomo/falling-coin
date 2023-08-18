using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundFade : MonoBehaviour
{
    AudioSource aud;

    // フェード
    float fadeInAlpha = 0.0f;
    // フェードイン処理をするかの判定
    bool isFadeIn = true;


    GameObject target;
    GameObject underFloor;

    float distance;

    const float kDistanceMax = 20;

    void Start()
    {
        aud = GetComponent<AudioSource>();
        target = GameObject.Find("Player");
        underFloor = GameObject.FindGameObjectWithTag("underFloor");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // フェードインをするかの判定
        if (isFadeIn)
        {
            fadeInAlpha += 0.015625f;
            // 立った時間がフェードイン時間たったら
            if (1.0f <= fadeInAlpha)
            {
                // フェードイン処理をしないようにする
                isFadeIn = false;
            }

            // ボリュームを徐々にあげるようにする
            aud.volume = fadeInAlpha;

        }
        else
        {
            // プレイヤーと最下部の地面との距離を測る
            distance = target.transform.position.y - underFloor.transform.position.y;

            // 距離が規定以内であればボリュームを下げていく
            if (distance < kDistanceMax)
            {
                aud.volume = distance / kDistanceMax;
            }
        }
    }
}
