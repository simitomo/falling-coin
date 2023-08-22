using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    string sceneName;
    public bool isRezult = false;
    public string materialNum;

    int sceneNo;
    bool isLoadNo = true;

    void Start()
    {
        if (!isRezult)
        {
            while(isLoadNo)
            {
                sceneNo = (Random.Range(0, 3) + 1);
                if (sceneNo != PlayerPrefs.GetInt("SceneNo", 0))
                {
                    isLoadNo = false;
                    PlayerPrefs.SetInt("SceneNo", sceneNo);
                }
            }

            // マップのロード名
            sceneName = "Map" + sceneNo.ToString() + "-" + materialNum;
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
