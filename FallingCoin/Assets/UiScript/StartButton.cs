using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // ボタンが押されたときに動作
    public void OnClick()
    {
        Debug.Log("StartPush");

        string mapString = "map" + (Random.Range(0, 3)+1).ToString() + "-1";

        SceneManager.LoadScene("map1-1");
    }
}
