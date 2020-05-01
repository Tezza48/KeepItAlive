using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : SpaceshipActor
{
    public Star star;

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FixedUpdateRotation(Input.GetAxis("Horizontal"));
        FixedUpdateThrust(Input.GetAxis("Vertical"));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var loot = collision.GetComponent<Loot>();
        if (loot != null)
        {
            var inv = loot.Consume();
            foreach(var elem in inv)
            {
                if (!inventory.ContainsKey(elem.Key)) inventory[elem.Key] = 0;

                inventory[elem.Key] =+ elem.Value;
            }
        }
    }
}
