using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rezult : MonoBehaviour
{
    int score;
    int maxScore;
    Text scorePointTxt;
    [SerializeField] GameObject scoreRank; 

    Vector2 size = Vector2.one;//new Vector2(2.0f, 2.0f);
    bool isUp = true;

    bool isEffect = true;

    void Start()
    {
        scorePointTxt = GetComponent<Text>();

        // スコアを代入
        score = PlayerPrefs.GetInt("Score");
        maxScore = PlayerPrefs.GetInt("MaxScore", 0);

        // 最高スコアの変更
        if (maxScore < score)
        {
            maxScore = score;
            PlayerPrefs.SetInt("MaxScore", maxScore);

            isEffect = true;
        }

        // スコア表記
        scorePointTxt.text = "MaxScore : " + maxScore.ToString() + "\nScore : " + score.ToString();

        // スコアランク表記
        if (score >= 100)
        {
            scoreRank.GetComponent<Text>().text = "S";
        }
        else if (score >= 80)
        {
            scoreRank.GetComponent<Text>().text = "A";
        }
        else if (score >= 60)
        {
            scoreRank.GetComponent<Text>().text = "B";
        }
        else
        {
            scoreRank.GetComponent<Text>().text = "C";
        }

        // スコアの初期化
        PlayerPrefs.SetInt("Score", 0);
        // タイムの初期化
        PlayerPrefs.SetInt("Time", 0);

        this.transform.localScale = size;
    }

    void FixedUpdate()
    {
        if (isEffect)
        {
            if (size.x >= 1.5f)
            {
                isUp = false;
            }
            if (size.x <= 1f)
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
}
