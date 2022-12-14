using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetShakeScript : MonoBehaviour
{
    public float shakeAmplitide;
    
    [SerializeField] private float shakeSize;


    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(
            Mathf.Lerp(-shakeAmplitide, shakeAmplitide, Mathf.PerlinNoise(Time.time * shakeSize, 0.0f)),
            0.0f, 0.0f);
    }
}
