using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour, IObject
{
    public float spinSpeed = 30;
    public float bobSpeed = 1;
    public float bobDistance = 1;
   /* Vector3 startPos = new Vector3();
    Vector3 newPos = new Vector3();
   */
    public int pointValue = 50;

    public GameManager gm;



    // Start is called before the first frame update
   void Awake ()
    {
        gm = FindObjectOfType<GameManager>();
    }

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

    private void OnTriggerEnter(Collider other) {
        switch (other.gameObject.tag)
        {
            case "Mine":
                Debug.Log("StarExplosion");
                Destroy(gameObject);
                break;
            case "Player":
                //add score
                Debug.Log("You got a star!");
                gm.ScoreChange(pointValue);
                Destroy(gameObject);
                break;
        }
    }

    public void Initiate(Transform player) { }
}
