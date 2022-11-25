using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{

    public GameObject explosion;


    private void OnTriggerEnter(Collider other)
    {
        Instantiate(explosion);
        Destroy(gameObject);
    }
}
