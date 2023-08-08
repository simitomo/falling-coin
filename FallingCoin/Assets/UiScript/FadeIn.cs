using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    AudioSource aud;
    // どのくらいの時間をかけてフェードインするか
    // 時間(S) * 50
    float fadeFrameTime = 200;
    // ステージに入ってからどのくらいたったか
    float fadeFrame = 0;
    // フェードイン処理をするかの判定
    bool isFadeIn = true;

    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // フェードインをするかの判定
        if (isFadeIn)
        {
            // たった時間を増やす
            fadeFrame++;
            // 立った時間がフェードイン時間たったら
            if (fadeFrame >= fadeFrameTime)
            {
                // 立った時間をフェードイン時間と同じにする
                fadeFrame = fadeFrameTime;
                // フェードイン処理をしないようにする
                isFadeIn = false;
            }

            // ボリュームを徐々にあげるようにする
            aud.volume = fadeFrame / fadeFrameTime;
        }
    }
}
