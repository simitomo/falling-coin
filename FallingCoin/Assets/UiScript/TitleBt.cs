using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleBt : MonoBehaviour
{
    // スタートに使う変数
    bool isStart = false;

    public GameObject countDown;
    public GameObject mainCamera;
    public GameObject fade;

    Text text;
    AudioSource aud;
    Image img;
    LoadScene scene;

    int countNo = 3;
    float alpha = 1.0f;

    // 説明ボタンに使う変数
    public GameObject canvas;
    public GameObject battenMark;
    public GameObject Guide1;
    GameObject nowInstance;

    // 終了用に使う変数
    public GameObject checkImg;
    GameObject imgInstance;

    public GameObject yesBt;
    public GameObject noBt;

    void Start()
    {
        text = countDown.GetComponent<Text>();
        aud = mainCamera.GetComponent<AudioSource>();
        img = fade.GetComponent<Image>();
        scene = GetComponent<LoadScene>();
    }
    void FixedUpdate()
    {
        if (isStart)
        {
            alpha -= 0.02f;

            if (alpha <= 0.0f)
            {
                countNo--;
                if (countNo < 0)
                {
                    isStart = false;

                    scene.LoadMap();    
                }
                else
                {
                    alpha = 1.0f;
                }
            }

            text.text = countNo.ToString();
            text.color = new Color(255, 255, 255, alpha);

            if (countNo == 0)
            {
                aud.volume = alpha;
                img.color = new Color(0, 0, 0, 1 - alpha);
            }
        }
    }

    // スタートボタン
    public void StartBt()
    {
        Debug.Log("StartPush");

        //string mapString = "map" + (Random.Range(0, 3)+1).ToString() + "-1";

        //SceneManager.LoadScene("map1-1");

        isStart = true;
        this.countDown.SetActive(true);
        this.fade.SetActive(true);
    }

    // 説明ボタンに使う関数
    public void GuideBt()
    {
        Debug.Log("GuidePush");

        battenMark.SetActive(true);
        nowInstance = Instantiate(Guide1);
        nowInstance.transform.SetParent(canvas.transform, false);
    }

    public void DestroyInstance()
    {
        battenMark.SetActive(false);

        Destroy(nowInstance);
    }

    public void ChangeInstance()
    {

    }

    // 終了ボタン
    public void QuitBt()
    {
        Debug.Log("QuitPush");

        Debug.Log("PUSH");

        imgInstance = Instantiate(checkImg);
        imgInstance.transform.SetParent(canvas.transform, false);
        yesBt.SetActive(true);
        noBt.SetActive(true);
    }

    public void YesBt()
    {
        // UNITY_EDITORをプレイしている時(デバック時)は上、
        // ビルド時は下が呼ばれる
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }

    public void NoBt()
    {
        Destroy(imgInstance);
        yesBt.SetActive(false);
        noBt.SetActive(false);
    }
}
