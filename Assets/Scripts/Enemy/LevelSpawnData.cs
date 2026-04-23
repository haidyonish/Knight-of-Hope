using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels/Spawn Data")]
public class LevelSpawnData : ScriptableObject
{
    public List<EnemyWave> waves = new List<EnemyWave>();
}