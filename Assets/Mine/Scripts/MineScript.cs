using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MineScript : MonoBehaviour, IObject
{
    [SerializeField] private GameObject explosion;
    public AudioClip explosionSFX;

    public void Initiate(Transform player) { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag( "Coin")){Destroy(other);}
        Instantiate(explosion, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(explosionSFX, transform.position, 0.5f);
        Destroy(gameObject);
    }

}
