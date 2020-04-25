using System;
using System.Collections.Generic;
using UnityEngine;

public class AIShip : SpaceshipActor
{
    public Transform target;

    [Serializable]
    public delegate StateFn StateFn();

    public StateFn currentState;

    private void Start()
    {
        currentState = IdleState;
    }

    public override void Update()
    {
        base.Update();

        currentState = currentState();
    }

    private StateFn IdleState()
    {
        if (Vector2.Distance(transform.position, target.position) < 20.0f)
        {
            //return State.Persuit;
        }


        return IdleState;
    }

    private StateFn PersuitState()
    {
        if (Vector2.Distance(transform.position, target.position) > 20.0f)
        {
            // TODO WT: Patrol
            return IdleState;
        }

        // Turn towards target
        // Move towards target
        return PersuitState;
    }
}
