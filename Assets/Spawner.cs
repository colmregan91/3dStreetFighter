using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private enemy[] enemies;
    [SerializeField] private Transform[] Positions;
    [SerializeField] private float SpawnTimer;
    [SerializeField] private float MaxSpawnCount = 10;
    [SerializeField] private float CurrentSpawnCount = 0;
    [SerializeField] private float SpawnRate = 1;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentSpawnCount >= MaxSpawnCount) return;

        SpawnTimer += Time.deltaTime;

        if (SpawnTimer >= SpawnRate)
        {
            CurrentSpawnCount++;
            SpawnTimer = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        int randomEnemyIndex = UnityEngine.Random.Range(0, enemies.Length);
        int randomPosIndex = UnityEngine.Random.Range(0, Positions.Length);

        var enemy = enemies[randomEnemyIndex].Get<enemy>(Positions[randomPosIndex].position, Quaternion.identity);
    }
}
