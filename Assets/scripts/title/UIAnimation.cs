using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimation : MonoBehaviour
{
    Image image;
    public Sprite[] sprites;//�A�j���[�V�����p�摜
    public float animSpeed;//�A�j���[�V�������x

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
