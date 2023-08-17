using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    string sceneName;
    public bool isRezult = false;
    public string materialNum;

    void Start()
    {
        if (!isRezult)
        {
            // マップのロード名
            sceneName = "map" + (Random.Range(0, 3) + 1).ToString() + "-" + materialNum;
        }
        else
        {
            // リザルトのロード名
            sceneName = "RezultSceen";
        }
    }

    public void LoadMap()
    {
        SceneManager.LoadScene(sceneName);
    }
}
