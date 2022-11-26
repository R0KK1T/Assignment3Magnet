using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetScript : MonoBehaviour
{
    //Frame
    private int magneticFrame = 10;
    private int currentFrame = 0;

    //Keypress
    public KeyCode activationKey;
    private bool keyIsPressed = false;

    //Magnet
    //private string magneticTag = "Magnetic";
    public List<string> magneticTags;
    private int maxDistance = 50; // MAGIC NUMBER
    private bool magnetIsActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForKeyPress();
    }
    private void FixedUpdate()
    {
        if (magnetIsActive)
        {
            if (currentFrame % magneticFrame == 0)
            {
                applyForceToMagneticObjects(getObjectsToPull());
                currentFrame = 0;
            }
            currentFrame++;
        }
    }

    void CheckForKeyPress()
    {
        if (Input.GetKeyDown(activationKey) && !keyIsPressed)
        {
            magnetIsActive = !magnetIsActive;
            currentFrame = 0;
            keyIsPressed = true;

            print("Key pressed");
        }
        else if (Input.GetKeyUp(activationKey) && keyIsPressed)
        {
            keyIsPressed = false;
        }
    }

    List<GameObject> getObjectsToPull()
    {
        List<GameObject> retList = new List<GameObject>();
        foreach (string tag in magneticTags)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject obj in gos)
            {
                Vector3 forward = transform.forward;//transform.TransformDirection(Vector3.forward);
                Vector3 toOther = (obj.transform.position - transform.position).normalized;

                //Debug.DrawRay(transform.position, forward, Color.blue, 10);
                //Debug.DrawRay(transform.position, toOther, Color.red, 10);


                if (Vector3.Dot(forward, toOther) > 0.8)
                {
                    print("The other transform is in the scope of the magnet!");
                    retList.Add(obj);
                }
            }
        }
        return retList;
    }

    void applyForceToMagneticObjects(List<GameObject> gameObjects)
    {
        foreach (GameObject obj in gameObjects)
        {
            float forceMult = maxDistance - Vector3.Distance(obj.transform.position, transform.position);
            //Debug.Log(forceMult);
            if (forceMult > 0)
            {
                obj.GetComponent<Rigidbody>().AddForce((transform.position - obj.transform.position).normalized * forceMult, ForceMode.Force);
            }
        }
    }
}
