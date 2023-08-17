using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    // スコアのアップ処理
    public void ScoreUp()
    {
        score += kScorePoint;
    }

    Text textScore;

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

    // オブジェクト終了時に動作
    void OnDestroy()
    {
        // スコアの値をScoreというデータに保存
        PlayerPrefs.SetInt("Score", score);
    }

    void Update()
    {
        // テキストを描画
        this.textScore.text = score.ToString();

        // デバック用コード
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
    }
}
