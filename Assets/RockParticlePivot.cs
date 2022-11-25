using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockParticlePivot : MonoBehaviour
{
    public Transform particle;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity != Vector3.zero)
            particle.forward = -rb.velocity;
    }
}
