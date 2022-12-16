using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MineScript : IObject
{
    [SerializeField] private GameObject explosion;

    protected override ObjectType Type => ObjectType.MINE;


    private bool hit = false;

    public override void Initiate(Transform player) { }

    public override void Hit(ObjectType type)
    {
        if (type == ObjectType.COIN || type == ObjectType.STAR) return;

        if (hit) return;

        hit = true;

        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
