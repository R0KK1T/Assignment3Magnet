using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetScreenInputScript : MonoBehaviour
{
    [SerializeField] private float swipeDist;
    [SerializeField] private float maxHeight;
    [SerializeField] private float maxWidth;

    private Animator animator;
    private MagnetScript magnetScript;


    private Vector2 touchStartPos;
    private bool shortTouch;
    private int prevDir;
    private bool touchStart;


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
                    Vector2 pos = NormaliseScreenPos(touch.position);
                    if (InBounds(pos))
                    {
                        touchStart = true;
                        shortTouch = true;
                        touchStartPos = pos;
                        prevDir = 0;
                    }
                    break;

                case TouchPhase.Moved:
                    if (touchStart)
                    {
                        Vector2 normPos = NormaliseScreenPos(touch.position);
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
                    break;

                case TouchPhase.Ended:
                    if (touchStart && shortTouch)
                    {
                        magnetScript.ToggleMagnet();
                    }
                    touchStart = false;
                    break;
            }
        }
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
