using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MagnetUIControl : MonoBehaviour
{
    public Camera cam;
    [Space]
    public float textScale;
    public float smoothTime = 0.3f;
    public float dist = 50;
    private Vector3 velocity = Vector3.zero;

    private Vector2 target;
    private Vector2 defaultTarget;

    private Vector2 offset;

    private Boolean holding = false;

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
                    holding = false;

                    target = defaultTarget;
                    offset = Vector2.zero;
                }
            }
        }
       
    }

    private void LateUpdate()
    {
        
    }
}
