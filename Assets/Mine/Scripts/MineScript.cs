using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MineScript : MonoBehaviour, IObject
{
    [SerializeField] private GameObject explosion;

    public void Initiate(Transform player) { }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
