using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceshipActor : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Light engineLight;
    public GameObject shootyThing;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
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

    public void Shoot()
    {
        if (shootyThing.activeSelf)
        {
            return;
        }

        shootyThing.SetActive(true);
        StartCoroutine(ShootTimeout(1.0f));
    }

    private IEnumerator ShootTimeout(float time)
    {
        yield return new WaitForSeconds(time);

        shootyThing.SetActive(false);
    }
}
