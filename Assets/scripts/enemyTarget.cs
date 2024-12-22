using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTarget : MonoBehaviour
{
    [Header("Scaling Settings")]
    public float shrinkSpeed = 2f; // �k�����x
    public GameObject enemyPrefab; // ��������G�̃v���t�@�u
    public Vector3 spawnOffset = Vector3.zero; // �G�̐����ʒu�̃I�t�Z�b�g

    void Update()
    {
        // ���݂̃X�P�[�����擾
        Vector3 currentScale = transform.localScale;

        // �X�P�[�������X�ɏ���������
        currentScale -= Vector3.one * shrinkSpeed * Time.deltaTime;

        // �X�P�[����0�����ɂȂ�Ȃ��悤����
        if (currentScale.x <= 0f)
        {
            currentScale = Vector3.zero;

            // �G�𐶐�
            SpawnEnemy();

            // ���g���폜
            Destroy(gameObject);
        }

        // �V�����X�P�[����K�p
        transform.localScale = currentScale;
    }

    void SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            // �G�𐶐��i���݂̈ʒu�ɃI�t�Z�b�g��������j
            Instantiate(enemyPrefab, transform.position + spawnOffset, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Enemy prefab is not assigned.");
        }
    }
}
