using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    float alpha = 1f;
    Image image;

    void Start()
    {
        image = GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        // スクリプトの破壊
        if (alpha <= 0)
        {
            Destroy(this);
            this.gameObject.SetActive(false);
        }

        alpha -= 0.015625f;
        if (alpha <= 0) alpha = 0;
        image.color = new Color(0, 0, 0, alpha);
    }
}
