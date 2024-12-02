using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{

    public GameObject[] spawnObjects;
    public bool stopSpawning = false;
    public float spawnTime;
    public float spawnDelay;

    void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    public void SpawnObject() {
        Instantiate(spawnObjects[Random.Range(0, spawnObjects.Length)], transform.position, transform.rotation);
        if(stopSpawning) {
            CancelInvoke("SpawnObject");
        }

    }
}
