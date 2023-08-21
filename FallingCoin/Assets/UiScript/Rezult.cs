using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rezult : MonoBehaviour
{
    int score;
    int maxScore;
    const int kClearScore = 1000;
    Text scorePointTxt;
    public GameObject canvas;
    int time;
    // 秒×50
    const int kMinTime = 6000;      // 現在２分
    const int kMaxTime = 15000;     // 現在５分

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
        // タイムを代入
        time = PlayerPrefs.GetInt("Time");

        score += kClearScore;

        if (time < kMinTime)
        {
            score = (int)(score * 1.5);
        }
        else if (kMaxTime < time)
        {
            score = (int)(score * (1 + ((kMaxTime - time) / kMaxTime) ) );
        }

        // スコアランク表記
        if (score >= 2000)
        {
            rankInstance = Instantiate(scoreRankS);
        }
        else if (score >= 1700)
        {
            rankInstance = Instantiate(scoreRankA);
        }
        else if (score >= 1300)
        {
            rankInstance = Instantiate(scoreRankB);
        }
        else
        {
            rankInstance = Instantiate(scoreRankC);
        }
        rankInstance.transform.SetParent(canvas.transform, false);

        // 最高スコアの変更
        if (maxScore < score)
        {
            maxScore = score;
            PlayerPrefs.SetInt("MaxScore", maxScore);

            isEffect = true;
        }

        // スコア表記
        scorePointTxt.text = "MaxScore : " + maxScore.ToString() + "\nScore : " + score.ToString();

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
