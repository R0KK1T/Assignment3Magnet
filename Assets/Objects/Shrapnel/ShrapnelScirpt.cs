using UnityEngine;

public class ShrapnelScirpt : IObject
{
    [SerializeField] private float explosionRadius;

    private Animator animator;
    private bool hit = false;

    protected override ObjectType Type => ObjectType.SHRAPNEL;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void Initiate(Transform player) { }

    public override void Hit(ObjectType type) 
    {
        if (type == ObjectType.COIN || type == ObjectType.STAR) return;

        if (hit) return;

        hit = true;

        animator.SetTrigger("Explode");
        Collider[] colls = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider coll in colls)
        {
            coll.GetComponent<IObject>()?.Hit(Type);
        }

    }

    public new void OnTriggerEnter(Collider other) { }

    public void FinishExplode()
    {
        Destroy(gameObject);
    }
}
