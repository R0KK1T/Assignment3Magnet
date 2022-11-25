using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetScript : MonoBehaviour
{
    //public GameObject player;
    private string magneticTag = "Magnetic";
    private int maxDistance = 50; // MAGIC NUMBER
    private int magneticFrame = 10;
    private int currentFrame = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentFrame % magneticFrame == 0)
        {
            applyForceToMagneticObjects(getObjectsToPull());
            currentFrame = 0;
        }
        currentFrame++;
    }

    List<GameObject> getObjectsToPull()
    {
        List<GameObject> retList = new List<GameObject>();
        GameObject[] gos = GameObject.FindGameObjectsWithTag(magneticTag);

        foreach (GameObject obj in gos)
        {
            Vector3 forward = transform.forward;//transform.TransformDirection(Vector3.forward);
            Vector3 toOther = (obj.transform.position - transform.position).normalized;

            Debug.DrawRay(transform.position, forward, Color.blue, 10);
            Debug.DrawRay(transform.position, toOther, Color.red, 10);


            if (Vector3.Dot(forward, toOther) > 0.8)
            {
                print("The other transform is in the scope of the magnet!");
                retList.Add(obj);
            }
        }
        return retList;
    }

    void applyForceToMagneticObjects(List<GameObject> gameObjects)
    {
        foreach (GameObject obj in gameObjects)
        {
            float forceMult = maxDistance - Vector3.Distance(obj.transform.position, transform.position);
            Debug.Log(forceMult);
            if (forceMult > 0)
            {
                obj.GetComponent<Rigidbody>().AddForce((transform.position - obj.transform.position).normalized * forceMult, ForceMode.Force);
            }
        }
    }
}
