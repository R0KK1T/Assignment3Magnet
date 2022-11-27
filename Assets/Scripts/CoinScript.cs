using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public float spinSpeed = 30;
    public float bobSpeed = 1;
    public float bobDistance = 1;
    Vector3 startPos = new Vector3();
    Vector3 newPos = new Vector3();

    public int pointValue = 10;


    // Start is called before the first frame update
    void Start()
    {

        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate coin
        transform.Rotate(new Vector3(0,20,0)*Time.deltaTime*spinSpeed);
        //bob up and down, position determined by a sine wave
        newPos=startPos;
        newPos.y += Mathf.Sin (Time.fixedTime*Mathf.PI*bobSpeed)*bobDistance;
        transform.position=newPos;
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Mine":
                //effect
                break;
            case "Player":
                //add score
                Debug.Log("You got a coin   !");
                Destroy(gameObject);
                break;
        }
    }



}
