using System;
using System.Collections.Generic;
using UnityEngine;

public class AIShip : SpaceshipActor
{
    public Transform target;

    const float AGGRO_DISTANCE = 30.0f;

    const float ATTACK_DISTANCE = 5.0f;

    [Serializable]
    public delegate StateFn StateFn();

    public StateFn currentState;

    private void Start()
    {
        currentState = IdleState;
    }

    public void FixedUpdate()
    {
        base.Update();

        currentState = currentState();
    }

    private StateFn IdleState()
    {
        if (Vector2.Distance(transform.position, target.position) < AGGRO_DISTANCE)
        {
            return PersuitState;
        }


        return IdleState;
    }

    private StateFn PersuitState()
    {
        var distance = Vector2.Distance(transform.position, target.position);

        if (distance > AGGRO_DISTANCE)
        {
            // TODO WT: Patrol
            return IdleState;
        } else if (distance < ATTACK_DISTANCE)
        {
            return AttackState;
        }



        // Turn towards target
        var (currentAngle, toTarget, angleToTarget) = GetAngleToTarget();

        FixedUpdateRotation(angleToTarget);

        // Move towards target
        FixedUpdateThrust(1.0f);

        return PersuitState;
    }

    private StateFn AttackState()
    {
        var distance = Vector2.Distance(transform.position, target.position);
        if (distance > ATTACK_DISTANCE + 2.0f)
        {
            return PersuitState;
        }

        // Turn towards target
        var (currentAngle, toTarget, angleToTarget) = GetAngleToTarget();

        FixedUpdateRotation(angleToTarget);

        Shoot();

        return AttackState;
    }

    private (float, Vector2, float) GetAngleToTarget()
    {
        var currentAngle = Mathf.Atan2(transform.up.y, transform.up.x);

        var toTarget = (target.position - transform.position).normalized;
        var angleToTarget = currentAngle - Mathf.Atan2(toTarget.y, toTarget.x);

        // TODO WT: Fix 360 spin when left of the AI.

        return (currentAngle, toTarget, angleToTarget);
    }

    private void OnGUI()
    {
        var (currentAngle, _, angleToTarget) = GetAngleToTarget();

        GUILayout.Label("currentAngle: " + currentAngle.ToString("N2"));
        GUILayout.Label("angleToTarget: " + angleToTarget.ToString("N2"));

        GUILayout.Label("Current State: " + currentState.Method.Name);
    }
}
