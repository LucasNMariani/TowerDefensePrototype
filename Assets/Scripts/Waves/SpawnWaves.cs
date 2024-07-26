using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnWaves : MonoBehaviour
{
    //public List<int> countEnemiesWaves = new List<int>(); //List of number of total waves and number of enemies to spawn in each one
    public List<WaveData> countEnemiesWaves = new List<WaveData>(); //List of number of total waves and number of enemies to spawn in each one

    /*[SerializeField]
    private List<EntityHealth> entityPrefab = new List<EntityHealth>(); //List of type of enemies to spawn*/
    [SerializeField]
    private Transform spawnPosition; //Position to spawn the wave
    [SerializeField]
    private float timeBetweenWaves; //Time to spawn the waves
    [SerializeField]
    private float timeBetweenEnemies; //Time between enemies spawn

    private bool canSpawnWaves;
    int _wavesCount = 1;

    public bool SetCanSpawn
    {
        set
        {
            canSpawnWaves = value;
        }
    }
   
    private void Start()
    {
        //StartCoroutine(SpawnWave());
        SetCanSpawn = true;
        GameManager.instance.UpdateWavesCountUI(_wavesCount);
    }

    public void OnClick(Button button) 
    {
        StartCoroutine(SpawnWave());
        button.interactable = false;
        GameManager.instance.SetActivePasiveMoney(true);
    }

    public IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        if (countEnemiesWaves.Count > 0 && canSpawnWaves == true)
        {
            Debug.Log("Enemigos de nueva oleada " + countEnemiesWaves[0]);
            for (int i = 0; i < countEnemiesWaves[0].enemyPrefabs.Length; i++)
            {
                SpawnEnemies(i);
                yield return new WaitForSeconds(timeBetweenEnemies);
            }
        }
    }
    public void NextRoundMethod()
    {
        StopAllCoroutines();
        countEnemiesWaves.RemoveAt(0);
        if (countEnemiesWaves.Count > 0)
        {
            StartCoroutine(SpawnWave());
            _wavesCount++;
            GameManager.instance.UpdateWavesCountUI(_wavesCount);
        }
        else
        {
            GameManager.instance.Win();
        }
    }

    void SpawnEnemies(int arrayIndex) 
    {
        Instantiate(countEnemiesWaves[0].enemyPrefabs[arrayIndex], spawnPosition.position, spawnPosition.rotation);
    }
    
}
