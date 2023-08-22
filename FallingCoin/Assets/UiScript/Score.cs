using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    

    Text textScore;

    int downScore = 100;

    // アイテムをとって上がるスコアの得点
    const int kScorePoint = 1000;

    // スコア用の変数
    int score;

    void Start()
    {
        this.textScore = GameObject.Find("Score").GetComponent<Text>();

        // スコアデータの獲得(Scoreというデータがない場合0を代入)
        score = PlayerPrefs.GetInt("Score", 0);
    }

    public void SetScore()
    {
        // スコアの値をScoreというデータに保存
        PlayerPrefs.SetInt("Score", score);
    }

    // スコアのアップ処理
    public void ScoreUp()
    {
        score += kScorePoint;
    }

    // スコアダウン処理
    public void ScoreDonw()
    {
        downScore = score / 4;
        if (500 < downScore)
            downScore = 500;

        score -= downScore;
    }

    void Update()
    {
        // テキストを描画
        this.textScore.text = score.ToString();


#if UNITY_EDITOR
        //デバック用コード
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKey(KeyCode.P))
            {
                score += 100;
            }
            if (Input.GetKey(KeyCode.O))
            {
                score = 0;
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                SceneManager.LoadScene("TashiroCreateScene2");
            }
        }
#endif
    }
}
