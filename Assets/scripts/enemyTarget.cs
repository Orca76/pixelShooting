using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTarget : MonoBehaviour
{
    [Header("Scaling Settings")]
    public float shrinkSpeed = 2f; // 縮小速度
    public GameObject enemyPrefab; // 生成する敵のプレファブ
    public Vector3 spawnOffset = Vector3.zero; // 敵の生成位置のオフセット

    void Update()
    {
        // 現在のスケールを取得
        Vector3 currentScale = transform.localScale;

        // スケールを徐々に小さくする
        currentScale -= Vector3.one * shrinkSpeed * Time.deltaTime;

        // スケールが0未満にならないよう制限
        if (currentScale.x <= 0f)
        {
            currentScale = Vector3.zero;

            // 敵を生成
            SpawnEnemy();

            // 自身を削除
            Destroy(gameObject);
        }

        // 新しいスケールを適用
        transform.localScale = currentScale;
    }

    void SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            // 敵を生成（現在の位置にオフセットを加える）
            Instantiate(enemyPrefab, transform.position + spawnOffset, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Enemy prefab is not assigned.");
        }
    }
}
