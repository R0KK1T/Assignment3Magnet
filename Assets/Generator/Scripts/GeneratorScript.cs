using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GeneratorScript : MonoBehaviour
{
    public bool MagnetActivate = false;

    [SerializeField] private Vector2 spawnChance = new(0.01f, 0.1f);
    [SerializeField] private Vector2 radiusRange = new(3.0f, 4.0f);

    [SerializeField] private ObjectChance[] objectChances;

    [SerializeField] private Transform player;

    public GameManager gm;
    private ARAnchorManager anchorManager;
    private ARPlaneManager planeManager;

    void Start()
    {
        anchorManager = GameObject.Find("AR Session Origin").GetComponent<ARAnchorManager>();
        planeManager = GameObject.Find("AR Session Origin").GetComponent<ARPlaneManager>();
    }

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
        Quaternion rot = Random.rotation;
        ARPlane plane = null;
        ARAnchor anchorPoint;
        foreach (ARPlane p in planeManager.trackables)
        {
            plane = p;
            break;
        }

        if (plane != null)
        {
            anchorPoint = anchorManager.AttachAnchor(plane, new Pose(pos, rot));
            Debug.Log("Added anchor to a plane");
        }
        else
        {
            anchorPoint = anchorManager.gameObject.AddComponent<ARAnchor>();
            Debug.Log("Added another anchor");
        }

        if (anchorPoint != null)
        {
            GameObject instantiated = Instantiate(obj, pos, rot);

            instantiated.GetComponent<IObject>().Initiate(transform);

            instantiated.transform.parent = anchorPoint.transform;
        }
    }
}


[System.Serializable]
struct ObjectChance
{
    public GameObject ObjectPrefab;

    public float Chance;
}