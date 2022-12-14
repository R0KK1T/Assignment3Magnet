using UnityEngine;

public class MagnetBallSize : MonoBehaviour
{
    /// <summary>
    /// Size range between which ball will fluctuate.
    /// </summary>
    [HideInInspector] public Vector2 noiseRange;

    [SerializeField] private float noiseSize;

    // Update is called once per frame
    void Update()
    {
        float size = Mathf.PerlinNoise(Time.time * noiseSize, 0.0f);
        size = Mathf.Lerp(noiseRange.x, noiseRange.y, size);

        transform.localScale = new Vector3(size, size, size);
    }
}
