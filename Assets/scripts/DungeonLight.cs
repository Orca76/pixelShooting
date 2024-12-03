using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DungeonLight : MonoBehaviour
{
    PlayerBase sc;
    public int a; // 現在のフロアを格納
    Light2D light2D; // Light 2Dコンポーネントの参照

    void Start()
    {
        // "Player"タグがついたGameObjectを検索してPlayerBaseコンポーネントを取得
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            sc = player.GetComponent<PlayerBase>();
        }

        // このオブジェクトのLight 2Dコンポーネントを取得
        light2D = GetComponent<Light2D>();

        if (light2D == null)
        {
            Debug.LogError("Light 2Dコンポーネントが見つかりませんでした。このスクリプトを適切なオブジェクトにアタッチしてください。");
        }
    }

    void Update()
    {
        if (sc != null && light2D != null)
        {
            a = sc.currentFloor; // 現在のフロア番号を取得

            // currentFloorに基づいてLight 2Dのintensityを設定
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
                    light2D.intensity = 1.0f; // デフォルト値
                    break;
            }
        }
    }
}
