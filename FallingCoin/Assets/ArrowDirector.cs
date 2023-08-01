using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowDirector : MonoBehaviour
{
    // 矢印を入れる用の変数
    [SerializeField] GameObject arrow;
    
    
    // ゲージ
    [SerializeField] GameObject gauge;


    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(this.arrow);
            Instantiate(this.gauge);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Destroy(this.arrow);
            Destroy(this.gauge);
        }
    }
}