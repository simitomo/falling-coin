using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDirector : MonoBehaviour
{
    // プレイヤーを入れる用の変数
    Transform player;
    // スクリーン座標を入れる用の変数
    Vector3 screenPos;

    // 矢印を入れる用の変数
    GameObject arrow;

    // 初めの回転角
    Quaternion startRotation;

    // クリックした時の位置
    Vector2 startPos;

    // クリックした時の位置から上にY軸の成分を伸ばす用の変数
    Vector2 vectorY;

    // クリックした地点から現在までの位置
    Vector2 endPos;

    // 角度
    float angle = 0f;
    // 外積用
    Vector3 outerProduct;

    void Start()
    {
        this.player = GameObject.Find("player").transform;

        this.arrow = GameObject.Find("Arrow");
        startRotation = arrow.transform.rotation;
    }

    void Update()
    {
        // 見えなくさせる
        this.arrow.transform.localScale = Vector2.zero;

        screenPos = Camera.WorldToScreenPoint();

        if (Input.GetMouseButtonDown(0))
        {
            // 初めにタップしたマウスの位置を保存
            startPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            // その時のマウスの位置を保存
            endPos = Input.mousePosition;
            // マウス位置から初めにタップした位置を引いたベクトル成分に変更
            endPos = new Vector2(endPos.x - startPos.x, endPos.y - startPos.y);

            // 角度を求めるため、X軸0,Y軸をendPosと同じ成分で保存
            // 必ず上側に向くようにendPosが上を向いてる時はそのまま、下を向いているときは-をかけて代入
            if (endPos.y > 0)   vectorY = new Vector2(0, endPos.y);
            else                vectorY = new Vector2(0, -endPos.y);

            // 回転角度を求める
            angle = Vector2.Angle(vectorY, endPos) + 180;
            // 外積を求めてどちら側に回転させるか決める
            outerProduct = Vector3.Cross(vectorY, endPos);

            // 回転させる
            if (outerProduct.z > 0)
            {
                this.arrow.transform.localRotation = startRotation * Quaternion.Euler(0, 0, angle);
            }
            else
            {
                this.arrow.transform.localRotation = startRotation * Quaternion.Euler(0, 0, -angle);
            }

            // 見えるようにさせる
            this.arrow.transform.localScale = Vector2.one;
        }
    }
}