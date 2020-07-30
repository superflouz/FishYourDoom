using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct WeightedObject
{
    public uint weight;
    public GameObject objectPrefab;
}

public class WOManager
{
    public static WeightedObject Draw(List<WeightedObject> pool)
    {
        uint totalWeight = 0;
        foreach (WeightedObject wObject in pool) {
            totalWeight += wObject.weight;
        }

        uint randomValue = (uint)Random.Range(0, (int)totalWeight);
        uint currentWeightCheck = 0;
        WeightedObject returnedWeightedObject = pool[0];

        foreach (WeightedObject wObject in pool) {
            currentWeightCheck += wObject.weight;

            if (currentWeightCheck > randomValue) {
                returnedWeightedObject = wObject;
                break;
            }
        }
        return returnedWeightedObject;
    }
}