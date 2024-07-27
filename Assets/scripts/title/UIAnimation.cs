using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour
{
    Image image;
    public Sprite[] sprites;//アニメーション用画像
    public float animSpeed;//アニメーション速度

    float t;
    public int currentIndex;
    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if(t > animSpeed)
        {
            currentIndex++;
            t = 0;
        }
        image.sprite = sprites[currentIndex%sprites.Length];
    }
}
