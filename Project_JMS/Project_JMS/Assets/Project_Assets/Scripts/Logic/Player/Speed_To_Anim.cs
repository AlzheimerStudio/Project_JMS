using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Animator))]
public class Speed_To_Anim : MonoBehaviour
{

    public Animator anim;
    public MovementController movementController;


    // Use this for initialization
    void Start()
    {
        if (anim == null)
        {

            anim = GetComponent<Animator>();

        }

    }

    // Update is called once per frame
    void Update()
    {

        anim.SetFloat("_Speed", movementController.CurrentSpeed * 10f);
    }
}
