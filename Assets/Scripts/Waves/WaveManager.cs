using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;
    public SpawnWaves spawnWaves;
    private int enemiesToKill;

    private void Awake()
    {
        instance = this; 
    }

    private void Start()
    {
        GameManager.instance.OnCompleteLevel += Win;
        GameManager.instance.OnDefeatLevel += Defeat;
    }

    public void DeathEnemyCount()
    {
        if (spawnWaves.countEnemiesWaves.Count > 0)
        {
            enemiesToKill++;
            Debug.Log("Murieron " + enemiesToKill + " enemigos");
            if (enemiesToKill >= spawnWaves.countEnemiesWaves[0].enemyPrefabs.Length)
            {
                enemiesToKill = 0;
                spawnWaves.NextRoundMethod();
            }
        }
    }

    public void Win()
    {
        spawnWaves.SetCanSpawn = false;
        Debug.Log("YOU WIN!");
    }
    
    public void Defeat()
    {
        spawnWaves.SetCanSpawn = false;
        Debug.Log("DEFEAT!");
    }
}
