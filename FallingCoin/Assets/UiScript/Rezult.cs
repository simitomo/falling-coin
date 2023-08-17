using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rezult : MonoBehaviour
{
    int score;
    int maxScore;
    Text txt;

    Vector2 size = Vector2.one;
    bool isUp = true;

    void Start()
    {
        txt = GetComponent<Text>();

        // スコアを代入
        score = PlayerPrefs.GetInt("Score");
        maxScore = PlayerPrefs.GetInt("MaxScore", 0);

        // 最高スコアの変更
        if (maxScore < score)
        {
            maxScore = score;
            PlayerPrefs.SetInt("MaxScore", maxScore);
        }

        // スコア表記
        txt.text = "MaxScore : " + maxScore.ToString() + "\nScore : " + score.ToString();

        // スコアの初期化
        PlayerPrefs.SetInt("Score", 0);
        // タイムの初期化
        PlayerPrefs.SetInt("Time", 0);
    }

    void FixedUpdate()
    {
        if (size.x >= 2f)
        {
            isUp = false;
        }
        if (size.x <= 1.5f)
        {
            isUp = true;
        }

        if (isUp)
        {
            size.x += 0.00625f;
            size.y += 0.00625f;
        }
        else
        {
            size.x -= 0.00625f;
            size.y -= 0.00625f;
        }

        this.transform.localScale = size;
    }
}
