using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    // クリックした時の位置
    Vector2 startPos;

    // クリックした地点から現在までの位置
    Vector2 endPos;

    // 押した地点から押している地点までの斜辺の距離
    float length = 0;
    float kLength = 600f;

    Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();

        startPos = Input.mousePosition;

    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        // その時のマウスの位置を保存
        endPos = Input.mousePosition;
        // マウス位置から初めにタップした位置を引いたベクトル成分に変更
        endPos = new Vector2(endPos.x - startPos.x, endPos.y - startPos.y);

        // ベクトルの大きさを表示
        // そのままでは大きすぎるため値を小さくする
        length = Mathf.Sqrt(endPos.x * endPos.x + endPos.y * endPos.y);

        if (kLength <= length)
        {
            length = kLength;
        }

        slider.value = length / kLength;
    }
}
