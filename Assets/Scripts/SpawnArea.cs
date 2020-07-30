using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public bool recomputePositionIfMissed = true;

    public uint numberOfObjectsToSpawn = 0;
    public List<WeightedObject> objectsToSpawn = new List<WeightedObject>();
    public uint noneOptionWeight = 0;
    public uint numberOfOptionalObjectsToSpawn = 0;
    public List<WeightedObject> optionalObjectsToSpawn = new List<WeightedObject>();

    private PolygonCollider2D area;
    private Vector2 minPoint = new Vector2(0, 0);
    private Vector2 maxPoint = new Vector2(0, 0);

    void Awake()
    {
        area = GetComponent<PolygonCollider2D>();
        WeightedObject noneOption;
        noneOption.weight = noneOptionWeight;
        noneOption.objectPrefab = null;
        optionalObjectsToSpawn.Add(noneOption);
    }

    // Start is called before the first frame update
    void Start()
    {
        ComputeMinAndMaxPoints();

        SpawnObjects();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ComputeMinAndMaxPoints()
    {
        minPoint.x = maxPoint.x = area.points[0].x;
        minPoint.y = maxPoint.y = area.points[0].y;

        foreach (Vector2 point in area.points) {
            if (minPoint.x > point.x) {
                minPoint.x = point.x;
            } else if (maxPoint.x < point.x) {
                maxPoint.x = point.x;
            }

            if (minPoint.y > point.y) {
                minPoint.y = point.y;
            } else if (maxPoint.y < point.y) {
                maxPoint.y = point.y;
            }
        }

        // Change points to a global representation
        minPoint.x += area.transform.position.x;
        maxPoint.x += area.transform.position.x;
        minPoint.y += area.transform.position.y;
        maxPoint.y += area.transform.position.y;
    }

    private void SpawnObjects()
    {
        area.enabled = true;

        for (int i = 0; i < numberOfObjectsToSpawn; i++) {
            SpawnObjectFromPool(objectsToSpawn);
        }

        if (optionalObjectsToSpawn.Count > 0) {
            for (int i = 0; i < numberOfOptionalObjectsToSpawn; i++) {
                SpawnOptionalObjectFromPool(optionalObjectsToSpawn);
            }
        }

        area.enabled = false;
    }

    private void SpawnObjectFromPool(List<WeightedObject> pool)
    {
        Vector2 position = ComputePosition();

        InstantiateObject(WOManager.Draw(pool).objectPrefab, position);
    }

    private void SpawnOptionalObjectFromPool(List<WeightedObject> pool)
    {
        GameObject prefab = WOManager.Draw(pool).objectPrefab;
        if (prefab == null) return;

        Vector2 position = ComputePosition();
        InstantiateObject(prefab, position);
    }

    private Vector2 ComputePosition()
    {
        Vector2 location;
        Vector2 closestPoint;
        do {
            location = new Vector2(Random.Range(minPoint.x, maxPoint.x), Random.Range(minPoint.y, maxPoint.y));
            closestPoint = area.ClosestPoint(location);
        } while(recomputePositionIfMissed && location != closestPoint);

        return closestPoint;
    }

    private void InstantiateObject(GameObject gameObject, Vector2 position)
    {
        GameObject newInstance = Instantiate(gameObject, area.transform, false);
        newInstance.transform.position = position;
    }
}
