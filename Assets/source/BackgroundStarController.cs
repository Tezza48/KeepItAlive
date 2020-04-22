using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundStarController : MonoBehaviour
{
    public float relativeScale = 1.0f;
    public float reactionAttenuation = 0.2f;

    public float reactivityPercent = 10.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var scale = relativeScale + Mathf.PerlinNoise(Time.time * reactivityPercent, 0) * reactionAttenuation;
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
