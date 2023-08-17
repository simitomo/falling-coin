using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouseBt : MonoBehaviour
{
    [SerializeField] GameObject canvas;

    [SerializeField] GameObject battenMark;
    [SerializeField] GameObject pouse;
    GameObject nowInstance;


    public void OnClick()
    {
        Debug.Log("GuidePush");

        Time.timeScale = 0.0f;

        battenMark.SetActive(true);
        nowInstance = Instantiate(pouse);
        nowInstance.transform.SetParent(canvas.transform, false);
    }

    public void DestroyInstance()
    {
        Time.timeScale = 1.0f;

        battenMark.SetActive(false);

        Destroy(nowInstance);
    }
}
