using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowDirector : MonoBehaviour
{
    // 生成したプレハブをキャンバス上に保存するための変数
    GameObject canvas;

    // 矢印を入れる用の変数
    [SerializeField] GameObject arrowPrefab;
    // ゲージ
    [SerializeField] GameObject gaugePrefab;

    GameObject instanceArrow;
    GameObject instanceGauge;

    void Start()
    {
        // Canvasを探してくる
        canvas = GameObject.Find("Canvas");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            instanceArrow = Instantiate(this.arrowPrefab);
            instanceGauge = Instantiate(this.gaugePrefab);

            // 複製したオブジェクトをCanvasに格納
            instanceArrow.transform.SetParent(canvas.transform, false);
            instanceGauge.transform.SetParent(canvas.transform, false);
        }
    }
}