using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFadeOut : MonoBehaviour
{
    AudioSource aud;
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

    void FixedUpdate()
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
