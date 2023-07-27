using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    GameObject player;
    Transform playerTransform;

    void Start()
    {
        player = GameObject.Find("Player");
        playerTransform= player.transform;
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, playerTransform.position.y, transform.position.z);
    }

}
