using UnityEngine;
using UnityEngine.UI;

public class MagnetEffectScript : MonoBehaviour
{
    [Header ("Values")]
    public bool magnetOn =  false;
    public bool attract = false;
    [Space]
    [Header("Settings")]
    [SerializeField] private float switchSpeed;
    [Space]
    [Header("Ball particles")]
    [SerializeField] private ParticleSystem magnetAttract;
    [SerializeField] private Vector2 emissionRange;
    [SerializeField] private Vector2 magnetRadialRange;
    [Space]
    [Header("Scrolling lines")]
    [SerializeField] private ParticleSystem pullLines;
    [SerializeField] private GameObject pullArea;
    [SerializeField] private GameObject repelArea;
    [SerializeField] private Material linesMat;
    [SerializeField] private Vector2 lineAlphaRange;
    [Space]
    [Header("Noise ball FX")]
    [SerializeField] private MagnetBallSize ballFX;
    [SerializeField] private Vector4 ballSizeRange;
    [Space]
    [Header("Pull radius indicator")]
    [SerializeField] private Image pullRadius;
    [SerializeField] private Vector2 pullRadiusAlphaRange;
    [SerializeField] private Color attractColor;
    [SerializeField] private Color repelColor;
    [Header("Magnet shake")]
    [SerializeField] private MagnetShakeScript magnetShake;
    [SerializeField] private Vector2 shakeAmplitude;


    private float _magnetIndex = 0.0f;
    private float _attractIndex = 0.0f;


    // Update is called once per frame
    void Update()
    {
        MagnetOnFunc();

        AttractFunc();
    }


    private void MagnetOnFunc()
    {
        if (magnetOn)
        {
            _magnetIndex += Time.deltaTime * switchSpeed;
        }
        else
        {
            _magnetIndex -= Time.deltaTime * switchSpeed;
        }
        _magnetIndex = Mathf.Clamp(_magnetIndex, 0.0f, 1.0f);


        // Ball particles
        var emission = magnetAttract.emission;
        emission.rateOverTime = Mathf.Lerp(emissionRange.x, emissionRange.y, _magnetIndex);

        // Scrolling lines
        linesMat.SetColor("_TintColor", new Color(
                1.0f, 1.0f, 1.0f,
                Mathf.Lerp(lineAlphaRange.x, lineAlphaRange.y, _magnetIndex)
            ));

        // Noise ball FX
        var t_range = new Vector2(
                Mathf.Lerp(ballSizeRange.x, ballSizeRange.z, _magnetIndex),
                Mathf.Lerp(ballSizeRange.y, ballSizeRange.w, _magnetIndex)
            );
        ballFX.noiseRange = t_range;

        // Pull radius indicator
        pullRadius.color = new Color(
                pullRadius.color.r, pullRadius.color.g, pullRadius.color.b, 
                Mathf.Lerp(pullRadiusAlphaRange.x, pullRadiusAlphaRange.y, _magnetIndex)
            );

        // Magnet shake
        magnetShake.shakeAmplitide = Mathf.Lerp(shakeAmplitude.x, shakeAmplitude.y, _magnetIndex);
    }

    private void AttractFunc() 
    {
        if (attract)
        {
            _attractIndex += Time.deltaTime * switchSpeed;
        }
        else
        {
            _attractIndex -= Time.deltaTime * switchSpeed;
        }
        _attractIndex = Mathf.Clamp(_attractIndex, 0.0f, 1.0f);



        // Ball particles
        var velocityOverLifetime = magnetAttract.velocityOverLifetime;
        velocityOverLifetime.radial = Mathf.Lerp(magnetRadialRange.x, magnetRadialRange.y, _attractIndex);

        // Scrolling lines
        repelArea.SetActive(_attractIndex < 0.5f);
        pullArea.SetActive(_attractIndex >= 0.5f);

        // Pull radius indicator
        pullRadius.color = new Color(
                Mathf.Lerp(repelColor.r, attractColor.r, _attractIndex),
                Mathf.Lerp(repelColor.g, attractColor.g, _attractIndex),
                Mathf.Lerp(repelColor.b, attractColor.b, _attractIndex),
                pullRadius.color.a
            );
    }
}
