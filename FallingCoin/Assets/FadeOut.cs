using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    AudioSource aud;
    GameObject player;
    GameObject underFloor;

    void Start()
    {
        player = GameObject.Find("player");
    }

    void FixedUpdate()
    {
        if (collision.gameObject.CompareTag("score"))
        {

        }
    }
}
