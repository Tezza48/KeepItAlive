using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public ElementMakeup[] initialInventory;
    public ElementInventory inventory;

    public Star star;
    public Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        inventory = new ElementInventory(initialInventory);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            star.AddElements(inventory);
            //inventory.Clear();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rigid.rotation -= Input.GetAxis("Horizontal") * Time.deltaTime * 300.0f;
        rigid.AddForce(transform.up * Input.GetAxis("Vertical") * Time.fixedDeltaTime * 1000.0f);
    }
}
