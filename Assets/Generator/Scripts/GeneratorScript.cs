using System.Linq;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    public bool MagnetActivate = false;

    [SerializeField] private Vector2 spawnChance = new(0.01f, 0.1f);
    [SerializeField] private Vector2 radiusRange = new(3.0f, 4.0f);

    [SerializeField] private ObjectChance[] objectChances;

    [SerializeField] private Transform player;

    public void FixedUpdate()
    {
        float chance = MagnetActivate ? spawnChance.y : spawnChance.x;
        if (Random.Range(0.0f, 1.0f) < chance)
        {
            float sum = objectChances.Sum(x => x.Chance);

            float randNumb = Random.Range(0.0f, sum);

            float current = 0;

            foreach (ObjectChance obj in objectChances)
            {
                current += obj.Chance;

                if (randNumb < current)
                {
                    Generate(obj.ObjectPrefab); 
                    return;
                }

            }
        }
    }

    private void Generate(GameObject obj)
    {
        float angle = Random.Range(0.0f, 360.0f);

        float radius = Random.Range(radiusRange.x, radiusRange.y);
        Vector3 pos = transform.position + (Quaternion.Euler(0.0f, angle, 0.0f) * Vector3.forward) * radius;

        GameObject instantiated = Instantiate(obj, pos, Random.rotation);

        instantiated.GetComponent<IObject>().Initiate(transform);
    }
}


[System.Serializable]
struct ObjectChance
{
    public GameObject ObjectPrefab;

    public float Chance;
}