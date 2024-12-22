using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class fadeText : MonoBehaviour
{
    public TextMeshProUGUI targetText; // �t�F�[�h������e�L�X�g
    public float fadeDuration = 2f; // �t�F�[�h�ɂ����鎞�ԁi�b�j
    public float startDelay = 0f; // �t�F�[�h�J�n�܂ł̒x������

    private float elapsedTime = 0f;
    private Color originalColor;

    void Start()
    {
        if (targetText == null)
        {
            Debug.LogError("Target Text is not assigned.");
            enabled = false;
            return;
        }

        originalColor = targetText.color; // ���̐F��ۑ�
    }

    void Update()
    {
        if (elapsedTime < startDelay)
        {
            elapsedTime += Time.deltaTime;
            return;
        }

        float fadeProgress = Mathf.Clamp01((elapsedTime - startDelay) / fadeDuration);
        Color newColor = originalColor;
        newColor.a = Mathf.Lerp(1f, 0f, fadeProgress);
        targetText.color = newColor;

        elapsedTime += Time.deltaTime;

        // �t�F�[�h������������X�N���v�g�𖳌���
        if (fadeProgress >= 1f)
        {
            enabled = false;
        }
    }
}
