using System.Collections;
using System.Collections.Generic;
using GameObjectsConfigs;
using Objects;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelPrefab : MonoBehaviour
{
    [SerializeField] private GunObject[] _guns = default;
    [SerializeField] private EnemySpawn _enemySpawn = default;
    [SerializeField] private SpawnWaveConfig _spawnWaveConfig = default;
    [SerializeField] private FingerMove _aim = default;
    [SerializeField] private LevelEndLine _levelEnd = default;
    
    public GunObject[] Guns  => _guns;
    public EnemySpawn EnemySpawn => _enemySpawn;
    public SpawnWaveConfig SpawnWaveConfig => _spawnWaveConfig;
    public FingerMove Aim => _aim;
    public LevelEndLine LevelEnd => _levelEnd;
}
