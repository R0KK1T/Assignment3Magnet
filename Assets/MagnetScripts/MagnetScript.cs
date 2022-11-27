using System;
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

    //Magnet
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
        CheckForKeyPress(activationKey, ToggleMagnet);
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
        else if (currentFrame > 0)
        {
            currentFrame = 0;
        }
    }

    void CheckForKeyPress(KeyCode key, Action methodName)
    {
        if (Input.GetKeyDown(key))
        {
            methodName();

            print("Key pressed");
        }
    }
    void ToggleMagnet()
    {
        magnetIsActive = !magnetIsActive;
    }

    List<GameObject> getObjectsToPull()
    {
        List<GameObject> retList = new List<GameObject>();
        foreach (string tag in magneticTags)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject obj in gos)
            {
                Vector3 forward = transform.forward;
                Vector3 toOther = (obj.transform.position - transform.position).normalized;

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

            if (forceMult > 0)
            {
                obj.GetComponent<Rigidbody>().AddForce((transform.position - obj.transform.position).normalized * forceMult, ForceMode.Force);
            }
        }
    }
}
