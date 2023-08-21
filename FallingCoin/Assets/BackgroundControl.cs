using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundControl : MonoBehaviour
{
    public GameObject backgroundPrefab;
    GameObject bg1;
    GameObject bg2;

    void Start()
    {
        bg1 = Instantiate(backgroundPrefab, new Vector3(-1f, 0.5f, 0), Quaternion.identity);
        bg2 = Instantiate(backgroundPrefab, new Vector3(37f, 0.5f, 0), Quaternion.identity);
    }

    void FixedUpdate()
    {
        bg1.transform.Translate(-0.1f, 0, 0);
        bg2.transform.Translate(-0.1f, 0, 0);

        if (bg1.transform.position.x <= -37f)
        {
            Destroy(bg1);
            bg1 = Instantiate(backgroundPrefab, new Vector3(37f, 0.5f, 0), Quaternion.identity);
        }
        if (bg2.transform.position.x <= -37f)
        {
            Destroy(bg2);
            bg2 = Instantiate(backgroundPrefab, new Vector3(37f, 0.5f, 0), Quaternion.identity);
        }
    }
}
