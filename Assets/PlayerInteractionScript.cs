using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionScript : IObject
{
    [SerializeField] private ObjectValue[] values;

    [SerializeField] private GameManager gm;

    private Dictionary<ObjectType, int> objectValues;

    protected override ObjectType Type => ObjectType.PLAYER;

    private void Start()
    {
        objectValues = new Dictionary<ObjectType, int>();
        foreach (var value in values)
        {
            objectValues.Add(value.obj, value.value);
        }
    }

    public override void Hit(ObjectType type)
    {
        Debug.Log(type);

        if (!objectValues.ContainsKey(type))
            return;

        gm.ScoreChange(objectValues[type]);
    }

    public override void Initiate(Transform player) { }
    
}

[System.Serializable]
struct ObjectValue
{
    public ObjectType obj;
    public int value;
}
