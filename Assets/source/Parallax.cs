using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float paralaxEffect = 0.9f;

    private float z;

    // Start is called before the first frame update
    void Start()
    {
        z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        var toOrigin = transform.parent.position;

        transform.position = toOrigin * paralaxEffect + Vector3.forward * z;
    }
}
