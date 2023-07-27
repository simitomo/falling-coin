using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    AudioSource aud;
    float fadeFrameTime = 500;
    bool isFadeIn = true;
    float fadeFrame = 0;

    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isFadeIn)
        {
            fadeFrame++;
            if (fadeFrame >= fadeFrameTime)
            {
                fadeFrame = fadeFrameTime;
                isFadeIn = false;
            }

            aud.volume = (float)(fadeFrame / fadeFrameTime);
        }
    }
}
