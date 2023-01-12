using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MineScript : IObject
{
    [SerializeField] private GameObject explosion;
    public AudioClip explosionSFX;
    protected override ObjectType Type => ObjectType.MINE;


    private bool hit = false;

    public override void Initiate(Transform player) { }

    public override void Hit(ObjectType type)
    {
        if (type == ObjectType.COIN || type == ObjectType.STAR) return;

        if (hit) return;

        hit = true;

        AudioSource.PlayClipAtPoint(explosionSFX, transform.position, 0.5f);
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
