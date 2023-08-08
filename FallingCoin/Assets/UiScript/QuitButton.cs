using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void OnClick()
    {
        Debug.Log("QuitPush");

        // UNITY_EDITORをプレイしている時(デバック時)は上、
        // ビルド時は下が呼ばれる
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }
}
