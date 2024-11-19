using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRandomizer : MonoBehaviour
{
    // インスペクタで設定するスプライトの配列
    public Sprite[] sprites;

    // ゲーム開始時に呼ばれる
    void Start()
    {
        AssignRandomSprite();
    }

    // ランダムなスプライトを選び、自身に適用するメソッド
    private void AssignRandomSprite()
    {
        if (sprites.Length == 0)
        {
            Debug.LogWarning("No sprites assigned in the inspector.");
            return;
        }

        // スプライトをランダムに選択
        int randomIndex = Random.Range(0, sprites.Length);
        Sprite selectedSprite = sprites[randomIndex];

        // SpriteRendererを取得し、選ばれたスプライトを適用
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = selectedSprite;
        }
        else
        {
            Debug.LogWarning("SpriteRenderer component not found on this GameObject.");
        }
    }
}
