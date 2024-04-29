using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class stairsBase : MonoBehaviour
{
    GameObject player;
    public float dist;
    GameObject backGround;
   public bool nextFloorGo;//次のフロアに行けるか

    public RawImage fadeOutBlack;//黒背景　どんどん黒くなるようにフェードアウト
    public float fadeSpeed;
    float newAlpha;
    Color currentColor;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        backGround = GameObject.FindWithTag("backGround");

    }

    // Update is called once per frame
    void Update()
    {
       
        if (nextFloorGo)
        {
        
             
           
            //while (fadeOutBlack.color.a < 1)
            //{
            //    //fadeOutBlack.color.a += fadeSpeed * Time.deltaTime;
            //    currentColor = fadeOutBlack.color;
            //    newAlpha += fadeSpeed * Time.deltaTime;
            //    fadeOutBlack.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
            //    Debug.Log("透明度" + fadeOutBlack.color.a);
               
            //}
            //Debug.Log("透明度が" + fadeOutBlack.color.a + "になったので画面遷移");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            ////
             StartFadeOut();

            //fadeOutBlack.GetComponent<RawImage>().
        }
        else
        {
            if (dist > Vector2.Distance(gameObject.transform.position, player.transform.position))
            {
                player.GetComponent<PlayerBase>().dynamicGuide.text = "降りる:Space";
                player.GetComponent<PlayerBase>().dynamicGuide.alpha = 1;
                if (Input.GetKeyDown(KeyCode.Space))//おりる
                {
                    //次のフロアへ
                    player.GetComponent<PlayerBase>().dynamicGuide.text = "";
                    DontDestroyOnLoad(player);
                    DontDestroyOnLoad(backGround);

                   
                    GunManager gunSc = GameObject.Find("GunSystem").GetComponent<GunManager>();

                    gunSc.nameText.SetActive(true);
                    gunSc.mpText.SetActive(true);
                    gunSc.damageText.SetActive(true);
                    gunSc.explainText.SetActive(true);

                    //  Sc.existCopy = gunSc.PlayerExistObj;
                    newAlpha = 0;
                    currentColor = fadeOutBlack.color;
                    fadeOutBlack.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
                    nextFloorGo = true;
                    // SceneManager.LoadScene(SceneManager.GetActiveScene().name);//シーン遷移
                }
            }
        }
    }
    public void StartFadeOut()
    {
        // コルーチンを開始してフェードアウトを実行
        StartCoroutine(FadeInCoroutine());
        Debug.Log("透明度が" + fadeOutBlack.color.a+"になったので画面遷移");
       
    }

    IEnumerator FadeInCoroutine()
    {
       // canvasGroup.alpha = 0f; // 透明度を最初に0に設定
        float newAlpha = 0;
        Color currentColor = fadeOutBlack.color;
        fadeOutBlack.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
        while (fadeOutBlack.color.a < 1)
        {
            //fadeOutBlack.color.a += fadeSpeed * Time.deltaTime;
             currentColor = fadeOutBlack.color;
            newAlpha += fadeSpeed * Time.deltaTime;
            fadeOutBlack.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
            Debug.Log("透明度" + fadeOutBlack.color.a);
            yield return null;
        }
        //yield return new WaitForSeconds(1);
        PlayerBase Sc = player.GetComponent<PlayerBase>();
        Sc.floorChange = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // 透明度が最大になったらシーンを移動する

    }
    // フェードアウトを実行するコルーチン
    IEnumerator FadeOutCoroutine()
    {
        // 透明度が 0 になるまで繰り返す
        while (fadeOutBlack.color.a > 0)
        {
            // 現在の透明度を取得
            Color currentColor = fadeOutBlack.color;

            // 透明度を徐々に減少させる
            float newAlpha = currentColor.a - fadeSpeed * Time.deltaTime;
            newAlpha = Mathf.Max(newAlpha, 0); // 透明度がマイナスにならないようにする

            // 新しい透明度を設定
            fadeOutBlack.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);

            // 1フレーム待つ
            yield return null;
            if (newAlpha == 0)
            {
                break;
            }
        }
        SceneManager.LoadScene("SceneManager.GetActiveScene().name");
    }
}
