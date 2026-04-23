using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    [Header("When wave starts")]
    public float startTime;

    [Header("Spawn")]
    public float spawnCooldown = 3f;
    public float spawnAcceleration = 0.05f;
    public float minCooldown = 1f;

    public int enemiesPerSpawn = 1;

    [Header("Enemies available")]
    public List<GameObject> enemyPrefabs = new List<GameObject>();
}