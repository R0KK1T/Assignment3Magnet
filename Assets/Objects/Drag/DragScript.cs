using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScript : IObject
{
    [HideInInspector] public Vector3 dragPosition;

    [SerializeField] private int health;
    [SerializeField] private float dragSpeed;
    [SerializeField] private float dragTime;
    [SerializeField] private Transform innerSphere;

    private bool beingDragged = false;
    private Vector3 smoothVel;
    private int currentHealth;

    private Animator animator;
    private Rigidbody rb;

    protected override ObjectType Type => ObjectType.DRAG;
    

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        currentHealth = health;
    }

    private void LateUpdate()
    {
        if (beingDragged)
        {
            transform.position = Vector3.SmoothDamp(transform.position, dragPosition, ref smoothVel, dragTime, dragSpeed);
        }
    }

    public void StartDrag()
    {
        rb.isKinematic = true;
        animator.SetTrigger("Open");

        smoothVel = Vector3.zero;
        beingDragged = true;
        dragPosition = transform.position;
    }

    public void EndDrag()
    {
        rb.isKinematic = false;
        rb.velocity = smoothVel;

        animator.SetTrigger("Close");

        beingDragged = false;
    }

    public override void Initiate(Transform player) { }

    public override void Hit(ObjectType type)
    {
        if (type == ObjectType.COIN || type == ObjectType.STAR) return;

        currentHealth--;

        if (currentHealth >= 0)
        {
            float size = (currentHealth * 1.0f) / (health * 1.0f);
            innerSphere.localScale = new Vector3(size, size, size);
        }

        if (currentHealth == 0)
        {
            animator.SetTrigger("Despawn");
        }
    }

    public void DeathEnd()
    {
        Destroy(gameObject);
    }
}
