using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : SpaceshipActor
{
    public ElementMakeup[] initialInventory;
    public ElementInventory inventory;

    public Star star;

    // Start is called before the first frame update
    void Start()
    {
        inventory = new ElementInventory(initialInventory);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FixedUpdateRotation(Input.GetAxis("Horizontal"));
        FixedUpdateThrust(Input.GetAxis("Vertical"));
    }
}
