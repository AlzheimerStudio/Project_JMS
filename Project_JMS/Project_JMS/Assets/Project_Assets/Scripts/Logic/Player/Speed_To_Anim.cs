using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Animator))]
public class Speed_To_Anim : MonoBehaviour
{

    [SerializeField] private Animator anim;
    private MovementController movementController;
    [SerializeField] private ParticleSystem playerTrail;

    void Start()
    {
        anim = GetComponent<Animator>();
        movementController = GameManager.instance.movementController;
        playerTrail = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        anim.SetFloat("_Speed", movementController.CurrentSpeed * 10f);

        // Controls particle trail

        float speed = movementController.CurrentSpeed;
        var main = playerTrail.main;
        main.startSpeed = speed.MapRangeClamped(0.1f, 30f, 0.1f, 5);
        var shape = playerTrail.shape;
        shape.radius = speed.MapRangeClamped(0.1f, 3f, .8f, .1f);

        Debug.Log(1f.MapRangeClamped(1, 3, 0, 10));
    }
}
