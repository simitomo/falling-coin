using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideButton : MonoBehaviour
{
    [SerializeField] GameObject canvas;

    [SerializeField] GameObject battenMark;
    [SerializeField] GameObject Guide1;
    GameObject nowInstance;


    public void OnClick()
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
}
