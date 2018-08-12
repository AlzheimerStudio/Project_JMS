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

        if (speed > 0)
        {

            var main = playerTrail.main;
            main.startSpeed = speed.MapRangeClamped(0f, 30f, 0.1f, 10);
            var shape = playerTrail.shape;
            shape.radius = Mathf.Clamp(speed.MapRangeClamped(0f, 20f, .8f, .1f), 0f, .8f);
        }
    }

    public void ResetTrail()
    {
        var main = playerTrail.main;
        main.startSpeed = 0f;
        var shape = playerTrail.shape;
        shape.radius = .8f;
    }
}
