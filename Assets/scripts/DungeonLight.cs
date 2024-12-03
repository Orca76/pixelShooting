using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DungeonLight : MonoBehaviour
{
    PlayerBase sc;
    public int a; // ���݂̃t���A���i�[
    Light2D light2D; // Light 2D�R���|�[�l���g�̎Q��

    void Start()
    {
        // "Player"�^�O������GameObject����������PlayerBase�R���|�[�l���g���擾
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            sc = player.GetComponent<PlayerBase>();
        }

        // ���̃I�u�W�F�N�g��Light 2D�R���|�[�l���g���擾
        light2D = GetComponent<Light2D>();

        if (light2D == null)
        {
            Debug.LogError("Light 2D�R���|�[�l���g��������܂���ł����B���̃X�N���v�g��K�؂ȃI�u�W�F�N�g�ɃA�^�b�`���Ă��������B");
        }
    }

    void Update()
    {
        if (sc != null && light2D != null)
        {
            a = sc.currentFloor; // ���݂̃t���A�ԍ����擾

            // currentFloor�Ɋ�Â���Light 2D��intensity��ݒ�
            switch (a)
            {
                case 1:
                    light2D.intensity = 0.8f;
                    break;
                case 2:
                    light2D.intensity = 0.6f;
                    break;
                case 3:
                    light2D.intensity = 0.4f;
                    break;
                case 4:
                    light2D.intensity = 0.2f;
                    break;
                case 5:
                    light2D.intensity = 0.5f;
                    break;
                default:
                    light2D.intensity = 1.0f; // �f�t�H���g�l
                    break;
            }
        }
    }
}
