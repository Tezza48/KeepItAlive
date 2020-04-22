using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceshipActor : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Light engineLight;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        engineLight.intensity = Mathf.Min(1, rigid.velocity.magnitude / 10.0f);
    }

    public void FixedUpdateRotation(float amount)
    {
        rigid.rotation -= amount * Time.deltaTime * 300.0f;
    }

    public void FixedUpdateThrust(float amount)
    {
        rigid.AddForce(transform.up * amount * Time.fixedDeltaTime * 1000.0f);
    }
}
