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
    public KeyCode reversePolarityKey;

    //Magnet
    public List<string> magneticTags;
    private int maxDistance = 50; // MAGIC NUMBER
    private bool magnetIsActive = false;
    private int polarity = 1;
    private Color magnetColor;
    private Color invertedMagnetColor;

    // Start is called before the first frame update
    void Start()
    {
        magnetColor = gameObject.GetComponentInChildren<MeshRenderer>().material.GetColor("_Color");
        invertedMagnetColor = InvertedColor(magnetColor);
    }
    Color InvertedColor(Color c)
    {
        return new Color(1.0f - c.r, 1.0f - c.g, 1.0f - c.b);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(activationKey))
        {
            ToggleMagnet();
        }
        if (Input.GetKeyDown(reversePolarityKey))
        {
            ReversePolarity();
        }
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

    void ToggleMagnet()
    {
        magnetIsActive = !magnetIsActive;
        print("Magnet is active: " + magnetIsActive);
    }
    void ReversePolarity()
    {
        polarity *= -1;
        if (polarity < 0)
        {
            gameObject.GetComponentInChildren<MeshRenderer>().material.SetColor("_Color", invertedMagnetColor);
        }
        else
        {
            gameObject.GetComponentInChildren<MeshRenderer>().material.SetColor("_Color", magnetColor);
        }
        print("Polarity is: " + polarity);
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
                obj.GetComponent<Rigidbody>().AddForce((transform.position - obj.transform.position).normalized * forceMult * polarity, ForceMode.Force);
            }
        }
    }
}
