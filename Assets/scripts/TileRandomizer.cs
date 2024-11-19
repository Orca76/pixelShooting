using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRandomizer : MonoBehaviour
{
    // �C���X�y�N�^�Őݒ肷��X�v���C�g�̔z��
    public Sprite[] sprites;

    // �Q�[���J�n���ɌĂ΂��
    void Start()
    {
        AssignRandomSprite();
    }

    // �����_���ȃX�v���C�g��I�сA���g�ɓK�p���郁�\�b�h
    private void AssignRandomSprite()
    {
        if (sprites.Length == 0)
        {
            Debug.LogWarning("No sprites assigned in the inspector.");
            return;
        }

        // �X�v���C�g�������_���ɑI��
        int randomIndex = Random.Range(0, sprites.Length);
        Sprite selectedSprite = sprites[randomIndex];

        // SpriteRenderer���擾���A�I�΂ꂽ�X�v���C�g��K�p
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
