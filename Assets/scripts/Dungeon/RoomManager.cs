using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject[] Gates = new GameObject[4];//è„âEâ∫ç∂
    public bool[] GateOn = new bool[4];//trueÇ≈ìñÇΩÇËîªíËñ≥ÇµÅ@triggerÇ≈îªï Ç∑ÇÈìsçáè„
    BoxCollider2D[] colliders = new BoxCollider2D[4];

    public GameObject[] RoomObjects;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            colliders[i] = Gates[i].GetComponent<BoxCollider2D>();
        }
        Instantiate(RoomObjects[Random.Range(0, RoomObjects.Length)], transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        for (int j = 0; j < 4; j++)
        {
            if (GateOn[j] == true)
            {
                colliders[j].isTrigger = true;
            }
            else
            {
                colliders[j].isTrigger = false;
            }
        }
    }
}
