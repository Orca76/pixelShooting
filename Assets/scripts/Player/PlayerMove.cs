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
    GameObject GunSystem;
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
        if (GunSystem.GetComponent<GunManager>().EditOn != true)//ï“èWíÜÇ≈Ç»Ç¢
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            rig.velocity = new Vector3(x, y, 0).normalized * speed;
            anim.runtimeAnimatorController = sc.walkAnim[sc.CharaIndex];
            if (rig.velocity != Vector2.zero)//à⁄ìÆíÜ
            {
                Debug.Log("INDEX=" + sc.CharaIndex);
                //anim.runtimeAnimatorController = sc.walkAnim[sc.CharaIndex];
                anim.speed = 0.75f;
            }
            else//í‚é~
            {
                anim.speed = 0;
            }
        }


    }
}
