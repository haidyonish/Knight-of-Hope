using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Level Data")]
    [SerializeField] private LevelSpawnData levelData;

    [Header("Scene References")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private PlayerXP playerXP;
    [SerializeField] private Transform player;
    [SerializeField] private Transform vip;

    private float nextSpawnTime = 0f;
    private float currentCooldown;

    private EnemyWave currentWave;

    private void Start()
    {
        if (levelData.waves.Count > 0)
        {
            currentWave = levelData.waves[0];
            currentCooldown = currentWave.spawnCooldown;
        }
    }

    private void Update()
    {
        UpdateCurrentWave();

        if (currentWave == null)
            return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemies(currentWave.enemiesPerSpawn);

            nextSpawnTime = Time.time + currentCooldown;

            currentCooldown =
                Mathf.Max(
                    currentCooldown - currentWave.spawnAcceleration,
                    currentWave.minCooldown
                );
        }
    }

    private void UpdateCurrentWave()
    {
        for (int i = levelData.waves.Count - 1; i >= 0; i--)
        {
            if (Time.time >= levelData.waves[i].startTime)
            {
                if (currentWave != levelData.waves[i])
                {
                    currentWave = levelData.waves[i];
                    currentCooldown = currentWave.spawnCooldown;
                }

                return;
            }
        }
    }

    private void SpawnEnemies(int count)
    {
        if (currentWave.enemyPrefabs.Count == 0)
            return;

        count = Mathf.Min(count, spawnPoints.Length);

        int[] indices = new int[spawnPoints.Length];

        for (int i = 0; i < indices.Length; i++)
            indices[i] = i;

        Shuffle(indices);

        for (int i = 0; i < count; i++)
        {
            Transform point = spawnPoints[indices[i]];

            GameObject prefab =
                currentWave.enemyPrefabs[
                    Random.Range(0, currentWave.enemyPrefabs.Count)];

            GameObject enemy =
                Instantiate(prefab, point.position, Quaternion.identity);

            enemy.GetComponent<EnemyHealth>().SetPlayerXP(playerXP);
            enemy.GetComponent<EnemyMovement>().SetTargets(player, vip);
        }
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