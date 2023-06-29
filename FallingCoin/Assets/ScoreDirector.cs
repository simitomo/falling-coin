using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;    // TextMeshProを使えるようにする

public class ScoreDirector : MonoBehaviour
{
    public void ScoreUp()
    {
        score += kScorePoint;
    }

    // TextMeshProを使う用の変数
    TextMeshProUGUI textScore;

    // アイテムをとって上がるスコアの得点
    const int kScorePoint = 1000;

    // スコア用の変数
    int score;

    void Start()
    {
        // TextMeshProを得る
        this.textScore = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();

        // スコアの初期化
        this.score = 0;
    }

    void FixedUpdate()
    {
        // テキストを変更して表示させる
        this.textScore.SetText("{0}", score);

        if (Input.GetMouseButtonDown(0))
        {
            score += 100;
        }
    }
}
