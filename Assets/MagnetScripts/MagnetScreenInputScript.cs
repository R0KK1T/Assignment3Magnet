using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetScreenInputScript : MonoBehaviour
{
    [SerializeField] private float swipeDist;
    [SerializeField] private float maxHeight;
    [SerializeField] private float maxWidth;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask touchMask;

    private Animator animator;
    private MagnetScript magnetScript;

    // Magnet vars
    private Vector2 touchStartPos;
    private bool shortTouch;
    private int prevDir;
    private bool touchStart;

    // Drag vars
    private bool isDragging = false;
    private DragScript dragObj;
    private float dragDist;

    // Start is called before the first frame update
    void Start()
    {
        magnetScript = GetComponent<MagnetScript>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (!isDragging)
                    {
                        Vector2 pos = NormaliseScreenPos(touch.position);
                        if (InBounds(pos))
                        {
                            MagnetTouchStart(pos);
                        }
                        else
                        {
                            CheckScreenObjects(touch.position);
                        }
                    }
                    break;

                case TouchPhase.Moved:
                    if (touchStart)
                    {
                        MagnetTouchMove(touch.position);
                    }

                    if (isDragging)
                    {
                        DragUpdate(touch.position);
                    }
                    break;

                case TouchPhase.Ended:
                    if (touchStart)
                    {
                        MagnetTouchEnd();
                    }

                    if (isDragging)
                    {
                        DragEnd();
                    }
                    break;
            }
        }
    }

    private void MagnetTouchStart(Vector2 pos)
    {
        touchStart = true;
        shortTouch = true;
        touchStartPos = pos;
        prevDir = 0;
    }

    private void MagnetTouchMove(Vector2 pos)
    {
        Vector2 normPos = NormaliseScreenPos(pos);
        float dist = normPos.x - touchStartPos.x;
        if (swipeDist < Mathf.Abs(dist) && dist * prevDir <= 0.0f)
        {
            shortTouch = false;
            touchStartPos = normPos;
            magnetScript.ReversePolarity();

            if (dist < 0.0f)
            {
                prevDir = -1;
                animator.SetTrigger("Left");
            }
            else
            {
                prevDir = 1;
                animator.SetTrigger("Right");
            }
        }
        else if (dist * prevDir > 0.0f && !Mathf.Approximately(dist * prevDir, 0.0f))
        {
            touchStartPos = normPos;
        }
    }

    private void MagnetTouchEnd()
    {
        if (touchStart && shortTouch)
        {
            magnetScript.ToggleMagnet();
        }
        touchStart = false;
    }


    private void CheckScreenObjects(Vector2 pos)
    {
        Ray ray = cam.ScreenPointToRay(pos);
        
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, touchMask))
        {
            if (hit.transform.CompareTag("Drag"))
            {
                DragStart(hit);
            }
        }
    }

    private void DragStart(RaycastHit hit)
    {
        dragObj = hit.transform.GetComponent<DragScript>();
        dragObj.StartDrag();

        dragDist = Vector3.Distance(transform.position, hit.transform.position);
        isDragging = true;

        if (touchStart)
        {
            MagnetTouchEnd();
        }
    }

    private void DragUpdate(Vector2 pos)
    {
        Ray ray = cam.ScreenPointToRay(pos);
        dragObj.dragPosition = ray.GetPoint(dragDist);
    }

    private void DragEnd()
    {
        isDragging = false;

        dragObj?.EndDrag();
    }


    private bool InBounds(Vector2 pos)
    {
        return pos.y < maxHeight && (pos.x > 0.5f - maxWidth * 0.5f && pos.x < 0.5f + maxWidth * 0.5f);
    }

    private Vector2 NormaliseScreenPos(Vector2 pos)
    {
        return new Vector2(pos.x / Screen.width, pos.y / Screen.height);
    }
}
