using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowDirector : MonoBehaviour
{
    GameObject arrow;
    GameObject arrowPower;

    Vector3 startPos;
    Vector3 powerPos;

    Vector3 shaft = new Vector3(0, 0, 1.0f);
    float angle = 0;

    // 以下デバック用
    Text debug;

    void Start()
    {
        arrow = GameObject.Find("Arrow");
        arrowPower = GameObject.Find("ArrowPower");
        startPos = Vector3.zero;
        powerPos = Vector3.zero;

        // 以下デバック用
        debug = GameObject.Find("Debug").GetComponent<Text>();
    }

    void Update()
    {
        arrow.transform.localScale = Vector2.zero;
        arrowPower.transform.localScale = Vector2.zero;

        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            powerPos = Input.mousePosition - startPos;
            //arrowPower.transform.localScale = new Vector2(Mathf.Abs(powerPos.x / 100), 1);

            //arrow.transform.localScale = new Vector2(1, 1);
            arrow.transform.localScale = new Vector2(1, powerPos.x / 100 + powerPos.y / 100);
            angle = Mathf.Atan2(powerPos.x, powerPos.y) * Mathf.Rad2Deg;
            arrow.transform.rotation = Quaternion.AngleAxis(angle, shaft);
        }

        // 以下デバック用
        debug.text = "angle : " + angle;
    }
}
