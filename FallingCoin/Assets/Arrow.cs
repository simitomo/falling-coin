using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Arrow : MonoBehaviour
{
    // プレイヤーを入れる用の変数
    Transform player;
    // 矢印の座標変更用の変数
    RectTransform arrowTransform;

    // 初めの回転角
    Quaternion startRotation;
    // クリックした時の位置
    Vector2 startPos;

    // クリックした地点から現在までの位置
    Vector2 endPos;
    // クリックした時の位置から上にY軸の成分を伸ばす用の変数
    Vector2 vectorY;

    // 角度
    float angle = 0f;
    // 外積用
    Vector3 outerProduct;

    void Start()
    {
        startPos = Input.mousePosition;
        startRotation = gameObject.transform.rotation;
        this.player = GameObject.Find("Player").transform;
        arrowTransform = GetComponent<RectTransform>();
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
        Debug.Log("[Arrow]OK1");

        // プレイヤーの座標の位置にUIを表示させるようにする
        // 第一引数にカメラの情報を
        // 第二引数に追従したいオブジェクトの座標を入手
        arrowTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, player.position);

        // その時のマウスの位置を保存
        endPos = Input.mousePosition;
        // マウス位置から初めにタップした位置を引いたベクトル成分に変更
        endPos = new Vector2(endPos.x - startPos.x, endPos.y - startPos.y);

        // 角度を求めるため、X軸0,Y軸をendPosと同じ成分で保存
        // 必ず上側に向くようにendPosが上を向いてる時はそのまま、下を向いているときは-をかけて代入
        if (endPos.y > 0) vectorY = new Vector2(0, endPos.y);
        else vectorY = new Vector2(0, -endPos.y);

        // 回転角度を求める
        angle = Vector2.Angle(vectorY, endPos) + 180;
        // 外積を求めてどちら側に回転させるか決める
        outerProduct = Vector3.Cross(vectorY, endPos);

        // 回転させる
        if (outerProduct.z > 0)
        {
            this.transform.localRotation = startRotation * Quaternion.Euler(0, 0, angle);
        }
        else
        {
            this.transform.localRotation = startRotation * Quaternion.Euler(0, 0, -angle);
        }

        Debug.Log("[Arrow]OK2");
    }
}
