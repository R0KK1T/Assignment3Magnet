using UnityEngine;

public abstract class IObject : MonoBehaviour
{
    protected abstract ObjectType Type
    {
        get;
    }

    public abstract void Initiate(Transform player);

    public abstract void Hit(ObjectType type);

    public void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IObject>()?.Hit(Type);
    }
}
