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
   public bool nextFloorGo;//���̃t���A�ɍs���邩

    public RawImage fadeOutBlack;//���w�i�@�ǂ�ǂ񍕂��Ȃ�悤�Ƀt�F�[�h�A�E�g
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
            //    Debug.Log("�����x" + fadeOutBlack.color.a);
               
            //}
            //Debug.Log("�����x��" + fadeOutBlack.color.a + "�ɂȂ����̂ŉ�ʑJ��");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            ////
             StartFadeOut();

            //fadeOutBlack.GetComponent<RawImage>().
        }
        else
        {
            if (dist > Vector2.Distance(gameObject.transform.position, player.transform.position))
            {
                player.GetComponent<PlayerBase>().dynamicGuide.text = "�~���:Space";
                player.GetComponent<PlayerBase>().dynamicGuide.alpha = 1;
                if (Input.GetKeyDown(KeyCode.Space))//�����
                {
                    //���̃t���A��
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
                    // SceneManager.LoadScene(SceneManager.GetActiveScene().name);//�V�[���J��
                }
            }
        }
    }
    public void StartFadeOut()
    {
        // �R���[�`�����J�n���ăt�F�[�h�A�E�g�����s
        StartCoroutine(FadeInCoroutine());
        Debug.Log("�����x��" + fadeOutBlack.color.a+"�ɂȂ����̂ŉ�ʑJ��");
       
    }

    IEnumerator FadeInCoroutine()
    {
       // canvasGroup.alpha = 0f; // �����x���ŏ���0�ɐݒ�
        float newAlpha = 0;
        Color currentColor = fadeOutBlack.color;
        fadeOutBlack.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
        while (fadeOutBlack.color.a < 1)
        {
            //fadeOutBlack.color.a += fadeSpeed * Time.deltaTime;
             currentColor = fadeOutBlack.color;
            newAlpha += fadeSpeed * Time.deltaTime;
            fadeOutBlack.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
            Debug.Log("�����x" + fadeOutBlack.color.a);
            yield return null;
        }
        //yield return new WaitForSeconds(1);
        PlayerBase Sc = player.GetComponent<PlayerBase>();
        Sc.floorChange = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // �����x���ő�ɂȂ�����V�[�����ړ�����

    }
    // �t�F�[�h�A�E�g�����s����R���[�`��
    IEnumerator FadeOutCoroutine()
    {
        // �����x�� 0 �ɂȂ�܂ŌJ��Ԃ�
        while (fadeOutBlack.color.a > 0)
        {
            // ���݂̓����x���擾
            Color currentColor = fadeOutBlack.color;

            // �����x�����X�Ɍ���������
            float newAlpha = currentColor.a - fadeSpeed * Time.deltaTime;
            newAlpha = Mathf.Max(newAlpha, 0); // �����x���}�C�i�X�ɂȂ�Ȃ��悤�ɂ���

            // �V���������x��ݒ�
            fadeOutBlack.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);

            // 1�t���[���҂�
            yield return null;
            if (newAlpha == 0)
            {
                break;
            }
        }
        SceneManager.LoadScene("SceneManager.GetActiveScene().name");
    }
}
