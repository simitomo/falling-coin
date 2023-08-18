using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rezult : MonoBehaviour
{
    int score;
    int maxScore;
    Text scorePointTxt;
    public GameObject canvas;

    public GameObject scoreRankS;
    public GameObject scoreRankA;
    public GameObject scoreRankB;
    public GameObject scoreRankC;

    GameObject rankInstance;

    Vector2 size = Vector2.one;//new Vector2(2.0f, 2.0f);
    bool isUp = true;

    bool isEffect = false;

    int effectCount = 0;
    const float kEffectSpeed = 0.0078125f;

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
            rankInstance = Instantiate(scoreRankS);
        }
        else if (score >= 80)
        {
            rankInstance = Instantiate(scoreRankA);
        }
        else if (score >= 60)
        {
            rankInstance = Instantiate(scoreRankB);
        }
        else
        {
            rankInstance = Instantiate(scoreRankC);
        }
        rankInstance.transform.SetParent(canvas.transform, false);

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
            if (3 <= effectCount) isEffect = false;

            if (size.x >= 1.4f)
            {
                isUp = false;
                effectCount++;
            }
            if (size.x <= 1f)
            {
                isUp = true;
            }

            if (isUp)
            {
                size.x += kEffectSpeed;
                size.y += kEffectSpeed;
            }
            else
            {
                size.x -= kEffectSpeed;
                size.y -= kEffectSpeed;
            }

            rankInstance.transform.localScale = size;
        }
        
    }
}
