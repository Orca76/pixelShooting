using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class fadeText : MonoBehaviour
{
    public TextMeshProUGUI targetText; // フェードさせるテキスト
    public float fadeDuration = 2f; // フェードにかかる時間（秒）
    public float startDelay = 0f; // フェード開始までの遅延時間

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

        originalColor = targetText.color; // 元の色を保存
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

        // フェードが完了したらスクリプトを無効化
        if (fadeProgress >= 1f)
        {
            enabled = false;
        }
    }
}
