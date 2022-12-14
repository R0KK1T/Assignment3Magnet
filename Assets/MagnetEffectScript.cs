using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetEffectScript : MonoBehaviour
{
    public bool magnetOn =  false;
    public bool attract = false;

    private float magnetIndex = 0.0f;

    private float attractIndex = 0.0f;


    // Update is called once per frame
    void Update()
    {
        if (magnetOn)
        {
            magnetIndex += Time.deltaTime;
        } 
        else
        {
            magnetIndex -= Time.deltaTime;
        }
        magnetIndex = Mathf.Clamp(magnetIndex, 0.0f, 1.0f);

        if (attract)
        {
            attractIndex += Time.deltaTime;
        }
        else
        {
            attractIndex -= Time.deltaTime;
        }
        attractIndex = Mathf.Clamp(attractIndex, 0.0f, 1.0f);
    }
}
