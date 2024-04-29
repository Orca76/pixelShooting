using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;

    Rigidbody2D rig;
    Animator anim;
    PlayerBase sc;
    public GameObject GunSystem;
    void Start()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sc = GetComponent<PlayerBase>();
        GunSystem = GameObject.Find("GunSystem");

    }

    // Update is called once per frame
    void Update()
    {
        if (!sc.isGameOver)//ゲームオーバーでない
        {
            if (!GunSystem)
            {
                GunSystem = GameObject.Find("GunSystem");
            }
            if (GunSystem.GetComponent<GunManager>().EditOn != true)//編集中でない
            {
                float x = Input.GetAxisRaw("Horizontal");
                float y = Input.GetAxisRaw("Vertical");

                rig.velocity = new Vector3(x, y, 0).normalized * speed;
                anim.runtimeAnimatorController = sc.walkAnim[sc.CharaIndex];
                if (rig.velocity != Vector2.zero)//移動中
                {
                   // Debug.Log("INDEX=" + sc.CharaIndex);
                    //anim.runtimeAnimatorController = sc.walkAnim[sc.CharaIndex];
                    anim.speed = 0.75f;
                }
                else//停止
                {
                    anim.speed = 0;
                }
            }
            else
            {
                rig.velocity = new Vector3(0, 0, 0).normalized * speed;
                anim.speed = 0;
            }

        }
        else
        {
            rig.velocity = new Vector3(0, 0, 0).normalized * speed;
            anim.speed = 0;
        }


    }
}
