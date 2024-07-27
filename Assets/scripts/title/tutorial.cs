using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class tutorial : MonoBehaviour
{
    public Canvas canvas;
    public TextMeshProUGUI tutorialText;
    void Update()
    {
        // マウスカーソルの位置を取得
        Vector2 mousePosition = Input.mousePosition;

        // PointerEventDataを作成
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = mousePosition
        };

        // GraphicRaycasterを取得
        GraphicRaycaster raycaster = canvas.GetComponent<GraphicRaycaster>();

        // Raycastを実行
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        // ヒットしたUI要素の情報を表示
        foreach (RaycastResult result in results)
        {
            gifExplain sc=result.gameObject.GetComponent<gifExplain>();
            if (sc != null)
            {
                tutorialText.text =Regex.Unescape (sc.Explain);
            }
            
            Debug.Log("Hovering over: " + result.gameObject.name);
        }
    }
}
