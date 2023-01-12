using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : IObject
{
    public float spinSpeed = 30;
    public float bobSpeed = 1;
    public float bobDistance = 1;
   /* Vector3 startPos = new Vector3();
    Vector3 newPos = new Vector3();
   */

    protected override ObjectType Type => ObjectType.COIN;

    // Update is called once per frame
    void Update()
    {
        
        //Rotate coin
        transform.Rotate(new Vector3(0,20,0)*Time.deltaTime*spinSpeed);
        //bob up and down, position determined by a sine wave
        /*
        newPos=startPos;
        newPos.y += Mathf.Sin (Time.fixedTime*Mathf.PI*bobSpeed)*bobDistance;
        transform.position=newPos;
        */
    }

    public override void Initiate(Transform player) { }

    public override void Hit(ObjectType type)
    {
        if (type != ObjectType.COIN || type != ObjectType.STAR)
            Destroy(gameObject);
    }
}
