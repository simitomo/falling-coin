using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    bool isStart = false;

    [SerializeField] GameObject countDown;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject fade;

    Text text;
    AudioSource aud;
    Image img;

    int countNo = 3;
    float alpha = 1.0f;

    void Start()
    {
        text = GetComponent<Text>();
        aud = mainCamera.GetComponent<AudioSource>();
        img = fade.GetComponent<Image>();

        fade.SetActive(true);
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

                    string mapString = "Map" + (Random.Range(0, 3) + 1).ToString() + "-1";

                    SceneManager.LoadScene(mapString);
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

    // ボタンが押されたときに動作
    public void OnClick()
    {
        Debug.Log("StartPush");

        //string mapString = "map" + (Random.Range(0, 3)+1).ToString() + "-1";

        //SceneManager.LoadScene("map1-1");

        isStart = true;
        this.countDown.SetActive(true);
    }
}
