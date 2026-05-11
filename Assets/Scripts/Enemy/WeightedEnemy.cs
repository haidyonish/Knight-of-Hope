using UnityEngine;

[System.Serializable]
public class WeightedEnemy
{
    public GameObject prefab;

    [Min(1)]
    public int weight = 1;
}