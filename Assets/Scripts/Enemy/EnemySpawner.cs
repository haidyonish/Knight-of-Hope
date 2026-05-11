using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private LevelSpawnData levelData;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private RunStatsManager runStatsManager;

    [Header("Scene References")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private PlayerXP playerXP;
    [SerializeField] private SoundManager soundManager;
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
            nextSpawnTime = 0f;
        }
    }

    private void Update()
    {
        UpdateCurrentWave();
        if (currentWave == null)
            return;
        if (gameManager.CurrentTime >= nextSpawnTime)
        {
            SpawnEnemies(currentWave.enemiesPerSpawn);
            nextSpawnTime = gameManager.CurrentTime + currentCooldown;
            currentCooldown = Mathf.Max(currentCooldown - currentWave.spawnAcceleration, currentWave.minCooldown);
        }
    }

    private void UpdateCurrentWave()
    {
        for (int i = levelData.waves.Count - 1; i >= 0; i--)
        {
            if (gameManager.CurrentTime >= levelData.waves[i].startTime)
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
        if (currentWave.enemies.Count == 0)
            return;
        count = Mathf.Min(count, spawnPoints.Length);
        int[] indices = new int[spawnPoints.Length];
        for (int i = 0; i < indices.Length; i++)
            indices[i] = i;
        Shuffle(indices);
        for (int i = 0; i < count; i++)
        {
            Transform point = spawnPoints[indices[i]];
            GameObject prefab = GetRandomEnemyPrefab();
            GameObject enemy = Instantiate(prefab, point.position, Quaternion.identity);
            EnemyHealth health = enemy.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.SetPlayerXP(playerXP);
                health.SetSoundManager(soundManager);
                health.SetRunStatsManager(runStatsManager);
                health.ApplyHealthMultiplier(currentWave.healthMultiplier);
            }
            EnemyCombat combat = enemy.GetComponent<EnemyCombat>();
            if (combat != null)
            {
                combat.SetSoundManager(soundManager);
                combat.ApplyDamageMultiplier(currentWave.damageMultiplier);
            }
            EnemyMovement movement = enemy.GetComponent<EnemyMovement>();
            if (movement != null)
            {
                movement.SetTargets(player, vip);
                movement.ApplySpeedMultiplier(currentWave.speedMultiplier);
            }
            RangedEnemyMovement rangedMovement = enemy.GetComponent<RangedEnemyMovement>();
            if (rangedMovement != null)
            {
                rangedMovement.SetTarget(vip);
                rangedMovement.ApplySpeedMultiplier(currentWave.speedMultiplier);
            }
            RangedEnemyCombat rangedCombat = enemy.GetComponent<RangedEnemyCombat>();
            if (rangedCombat != null)
            {
                rangedCombat.SetSoundManager(soundManager);
                rangedCombat.ApplyDamageMultiplier(currentWave.damageMultiplier);
            }
        }
    }

    private GameObject GetRandomEnemyPrefab()
    {
        int totalWeight = 0;
        foreach (var enemy in currentWave.enemies)
            totalWeight += enemy.weight;
        int random = Random.Range(0, totalWeight);
        int current = 0;
        foreach (var enemy in currentWave.enemies)
        {
            current += enemy.weight;
            if (random < current)
                return enemy.prefab;
        }
        return currentWave.enemies[0].prefab;
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