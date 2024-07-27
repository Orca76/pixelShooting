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
        // �}�E�X�J�[�\���̈ʒu���擾
        Vector2 mousePosition = Input.mousePosition;

        // PointerEventData���쐬
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = mousePosition
        };

        // GraphicRaycaster���擾
        GraphicRaycaster raycaster = canvas.GetComponent<GraphicRaycaster>();

        // Raycast�����s
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        // �q�b�g����UI�v�f�̏���\��
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
