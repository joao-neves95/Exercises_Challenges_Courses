using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsUIController : MonoBehaviour
{
    private Animator anim;

    //Use this for initialization
   void Start ()
   {
        anim = GetComponent<Animator>();
   }

    //Update is called once per frame

    void FixedUpdate()
    {
        anim.SetInteger("Health", PlayerController.PlayerHealth);
    }
}
