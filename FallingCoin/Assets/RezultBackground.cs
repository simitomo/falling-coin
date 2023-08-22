using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RezultBackground : MonoBehaviour
{
    public GameObject backgroundPrefab;
    GameObject bg1;
    GameObject bg2;

    void Start()
    {
        bg1 = Instantiate(backgroundPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        bg2 = Instantiate(backgroundPrefab, new Vector3(0, 10f, 0), Quaternion.identity);
    }

    void FixedUpdate()
    {
        bg1.transform.Translate(0, -0.03125f, 0);
        bg2.transform.Translate(0, -0.03125f, 0);

        //if (bg1.transform.position.x <= -19f)
        //{
        //    Destroy(bg1);
        //    bg1 = Instantiate(backgroundPrefab, new Vector3(19f, 0, 0), Quaternion.identity);
        //}
        //if (bg2.transform.position.x <= -19f)
        //{
        //    Destroy(bg2);
        //    bg2 = Instantiate(backgroundPrefab, new Vector3(19f, 0, 0), Quaternion.identity);
        //}

        if (bg1.transform.position.y <= -10f)
        {
            Destroy(bg1);
            bg1 = Instantiate(backgroundPrefab, new Vector3(0, 10f, 0), Quaternion.identity);
        }
        if (bg2.transform.position.y <= -10f)
        {
            Destroy(bg2);
            bg2 = Instantiate(backgroundPrefab, new Vector3(0, 10f, 0), Quaternion.identity);
        }
    }
}
