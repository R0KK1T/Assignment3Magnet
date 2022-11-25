using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour, IObject
{
    [SerializeField] private Vector2 velocityRange;
    [SerializeField] private Vector2 angleRange;
    [Space]
    [SerializeField] private float maxDistance = 15.0f;
    [SerializeField] private float maxTime = 5.0f;


    private Vector3 center;

    private Rigidbody rb;

    private Animator animator;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        StartCoroutine(Timer(maxTime));
    }

    public void Update()
    {
        if (Vector3.Distance(transform.position, center) > maxDistance)
        {
            Despawn();
        }
    }

    public void Initiate(Transform player)
    {
        Vector3 target = new Vector3(player.position.x, transform.position.y, player.position.z) - transform.position;

        float angle = Random.Range(angleRange.x, angleRange.y);
        if (Random.Range(0.0f, 1.0f) < 0.5f)
            angle *= -1.0f;

        Vector3 direction = (Quaternion.Euler(0.0f, angle, 0.0f) * target).normalized;

        rb.AddForce(direction * Random.Range(velocityRange.x, velocityRange.y), ForceMode.VelocityChange);

        center = player.position;
    }

    private void Despawn()
    {
        animator.SetTrigger("Despawn");
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        Despawn();
    }
}
