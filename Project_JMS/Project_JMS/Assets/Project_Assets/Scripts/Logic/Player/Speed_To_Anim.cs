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
    [SerializeField] private ParticleSystem ambientParticles;
    private ParticleSystem.VelocityOverLifetimeModule velocityModule;//settings container voor velocity over lifetime module.
    private ParticleSystem.VelocityOverLifetimeModule velocityModule2;//settings container voor velocity over lifetime module.

    private ParticleSystem.TrailModule trailModule;//settings container voor trail module.


    void Start()
    {
        anim = GetComponent<Animator>();
        movementController = GameManager.instance.movementController;
        playerTrail = GetComponentInChildren<ParticleSystem>();

        velocityModule = ambientParticles.velocityOverLifetime;
        velocityModule2 = playerTrail.velocityOverLifetime;

        trailModule = ambientParticles.trails;

    }

    void Update()
    {

        // Controls particle trail
        float speed = movementController.CurrentSpeed;
        anim.SetFloat("_Speed", speed * 10f);


        velocityModule.speedModifier = speed.MapRangeClamped(0f, 30f, 1f, 10f);
        trailModule.ratio = speed.MapRangeClamped(0f, 1f, 0.2f, speed / 100);

        if (speed > 0 && playerTrail != null && ambientParticles != null)
        {

            var main = playerTrail.main;
            main.startSpeed = speed.MapRangeClamped(0f, 30f, 0.1f, 50);
            var shape = playerTrail.shape;
            shape.radius = Mathf.Clamp(speed.MapRangeClamped(0f, 20f, .8f, .1f), 0f, .8f);

            //   velocityModule2.speedModifier = speed.MapRangeClamped(0f, 30, 0.1f, 50);





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
