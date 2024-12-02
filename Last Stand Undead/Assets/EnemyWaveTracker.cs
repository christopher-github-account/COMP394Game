using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class EnemyWaveTracker : MonoBehaviour
{

    public int enemiesKilled;
    
    void OnDestroy()
    {
        if (GameObject.FindGameObjectWithTag("WaveSpawner") != null)
        {
            GameObject.FindGameObjectWithTag("WaveSpawner").GetComponent<WaveSpawner>().spawnedEnemies.Remove(gameObject);
            enemiesKilled++;

        }
     
    }
}