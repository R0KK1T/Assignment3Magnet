using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MagnetUIControl : MonoBehaviour
{
    public Camera cam;
    public Transform shadow;
    public TextMeshProUGUI text;
    public MenuScript levelScript;
    [Space]
    public float levelDist;
    [Space]
    public float textScale;
    public float shadowScale;
    public Vector3 shadowDefaultOffset;
    public float smoothTime = 0.3f;
    public float dist = 50;
    private Vector3 velocity = Vector3.zero;

    private Vector2 target;
    private Vector2 defaultTarget;

    private Vector2 offset;

    private bool holding = false;

    // Start is called before the first frame update
    void Start()
    {
        defaultTarget = cam.WorldToScreenPoint(transform.position);
        target = defaultTarget;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 t_target = cam.ScreenToWorldPoint(((Vector3) (target + offset)) + Vector3.forward * dist);

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, t_target, ref velocity, smoothTime);

        
        
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(i))
                {
                    holding = true;
                    target = touch.position;

                    offset = ((Vector2) cam.WorldToScreenPoint(transform.position)) - target;
                }
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (holding)
                {
                    target = touch.position;
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if (holding)
                {
                    float t_dist = (target - defaultTarget).magnitude;
                    t_dist /= (Screen.width * 0.5f);
                    if (t_dist > levelDist)
                    {
                        levelScript.StartGame();
                    }

                    holding = false;

                    target = defaultTarget;
                    offset = Vector2.zero;
                }
            }
        }
       
    }

    private void LateUpdate()
    {
        Vector2 currentPos = cam.WorldToScreenPoint(transform.position);

        float t_dist = (currentPos - defaultTarget).magnitude;
        t_dist /= (Screen.width * 0.5f);

        shadow.localPosition = shadowScale * t_dist * (defaultTarget - currentPos).normalized;
        shadow.localPosition += Vector3.forward * 0.03f + shadowDefaultOffset;

        text.characterSpacing = textScale * t_dist;
    }
}
