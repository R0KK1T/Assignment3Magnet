using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBallSize : MonoBehaviour
{

    public bool On;

    [SerializeField] private float noiseSize;
    [SerializeField] private Vector2 noiseRange;

    // Update is called once per frame
    void Update()
    {
        float size = Mathf.PerlinNoise(Time.time * noiseSize, 0.0f);
        size = Mathf.Lerp(noiseRange.x, noiseRange.y, size);

        transform.localScale = new Vector3(size, size, size);
    }
}
