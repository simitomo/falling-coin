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

    float screenFade = 1.0f;
    float kScreenFadeOut = 3f;
    public GameObject fade;
    Image fadeImg;
    RectTransform rectPos;
    Vector2 fadeOutPos = new Vector2(0, 512f);
    Vector2 fadeInPos = new Vector2(-8f, -8f);

    GameObject target;
    GameObject underFloor;

    float distance;

    const float kDistanceMax = 20;

    void Start()
    {
        fadeImg = fade.GetComponent<Image>();
        rectPos = fade.GetComponent<RectTransform>();
        rectPos.localPosition = fadeInPos;

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

            screenFade -= 0.0625f;
            if (screenFade <= 0)
            {
                screenFade = 0;
                rectPos.localPosition = fadeOutPos;
            }
            fadeImg.color = new Color(0, 0, 0, screenFade);
        }
        else
        {
            // プレイヤーと最下部の地面との距離を測る
            distance = target.transform.localPosition.y - underFloor.transform.position.y;

            // 距離が規定以内であればボリュームを下げていく
            if (distance < kDistanceMax)
            {
                aud.volume = distance / kDistanceMax;
            }

            if (distance < kScreenFadeOut)
            {
                rectPos.localPosition = fadeInPos;
                fadeImg.color = new Color(0, 0, 0, 1 - distance / kScreenFadeOut);
            }
        }
    }
}
