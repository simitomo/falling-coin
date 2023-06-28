using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;    // TextMeshProを使えるようにする

public class ScoreDirector : MonoBehaviour
{
    // TextMeshProを使う用の変数
    TextMeshProUGUI textScore;

    // スコア用の変数
    int score;

    void Start()
    {
        this.textScore = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        // TextMeshProを得る
        this.score = 0;
    }

    void FixedUpdate()
    {
        // テキストを変更して表示させる
        this.textScore.text("Score : {0}", score);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            score++;
        }
    }
}
