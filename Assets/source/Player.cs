using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            star.AddElements(inventory);
            inventory.Clear();
        }
    }
}
