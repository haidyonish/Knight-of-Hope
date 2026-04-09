using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    private List<GameObject> enemyPrefabs;
    [SerializeField] private GameObject peasantPitchfork;
    [SerializeField] private GameObject peasantSickle;
    [SerializeField] private GameObject peasantKnife;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private PlayerXP playerXP;
    [SerializeField] private Transform player;
    [SerializeField] private Transform vip;

    [Header("First Phase")]
    [SerializeField] private float currentSpawnCooldown = 3f;
    [SerializeField] private float spawnAcceleration = 0.04f;
    [SerializeField] private float minSpawnCooldown = 1f;
    [SerializeField] private int enemiesPerSpawn = 1;

    [Header("Second Phase")]
    [SerializeField] private float phase2Time = 90f;
    [SerializeField] private float phase2SpawnCooldown = 10f;
    [SerializeField] private float phase2SpawnAcceleration = 0.25f;
    [SerializeField] private float phase2MinSpawnCooldown = 5f;
    [SerializeField] private int phase2EnemiesPerSpawn = 3;

    [Header("Third Phase")]
    [SerializeField] private float phase3Time = 180f;
    [SerializeField] private float phase3SpawnCooldown = 11.5f;
    [SerializeField] private float phase3SpawnAcceleration = 0.5f;
    [SerializeField] private float phase3MinSpawnCooldown = 5f;
    [SerializeField] private int phase3EnemiesPerSpawn = 5;

    private int currentPhase = 1;

    private float nextSpawnTime = 0;

    private void Awake()
    {
        enemyPrefabs = new List<GameObject>();
        enemyPrefabs.Add(peasantPitchfork);
    }

    private void Update()
    {
        if (currentPhase == 1 && Time.time > phase2Time)
            StartPhase2();
        if (currentPhase == 2 && Time.time > phase3Time)
            StartPhase3();


        if (Time.time > nextSpawnTime)
        {
            SpawnEnemy(enemiesPerSpawn);
            nextSpawnTime = Time.time + currentSpawnCooldown;
            currentSpawnCooldown = Mathf.Max(currentSpawnCooldown - spawnAcceleration, minSpawnCooldown);
        }
    }

    private void SpawnEnemy(int count)
    {
        int n = spawnPoints.Length;
        int[] indices = new int[n];
        for (int i = 0; i < n; i++)
            indices[i] = i;
        Shuffle(indices);
        count = Mathf.Min(count, spawnPoints.Length);
        for (int i = 0; i < count; i++)
        {
            Transform spawnPoint = spawnPoints[indices[i]];
            GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], spawnPoint.position, Quaternion.identity);
            enemy.GetComponent<EnemyHealth>().SetPlayerXP(playerXP);
            enemy.GetComponent<EnemyMovement>().SetTargets(player, vip);
        }
    }

    private void StartPhase2()
    {
        currentPhase = 2;
        currentSpawnCooldown = phase2SpawnCooldown;
        spawnAcceleration = phase2SpawnAcceleration;
        minSpawnCooldown = phase2MinSpawnCooldown;
        enemiesPerSpawn = phase2EnemiesPerSpawn;
        enemyPrefabs.Add(peasantSickle);
    }

    private void StartPhase3()
    {
        currentPhase = 3;
        currentSpawnCooldown = phase3SpawnCooldown;
        spawnAcceleration = phase3SpawnAcceleration;
        minSpawnCooldown = phase3MinSpawnCooldown;
        enemiesPerSpawn = phase3EnemiesPerSpawn;
        enemyPrefabs.Add(peasantKnife);
    }

    private void Shuffle(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (array[i], array[j]) = (array[j], array[i]);
        }
    }
}
