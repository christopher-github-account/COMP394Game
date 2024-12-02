using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class ItemDrop : MonoBehaviour
{
    public GameObject[] itemList;
    private int itemIndex;
    private int totalItemsInArray = 0;
    private Transform enemyPos;

    [Header("PowerUp Drops")]
    [SerializeField] private PowerUpEvents[] _PowerUpEvents;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        foreach(GameObject item in itemList)
        {
            totalItemsInArray++;
        }
        itemIndex = Random.Range(0, totalItemsInArray);
    }

    // Update is called once per frame
    private void Update()
    {
        if(EnemyHealth.enemyIsDead == true)
        {
            {
            DetermineIfDrop();
            }
        }
    }

    private void DetermineIfDrop()
    {
        float totalChance = 0f;
        foreach (PowerUpEvents interactableEvents in _PowerUpEvents)
        {
            totalChance += interactableEvents.DropChance;
        }

        float rand = Random.Range(0f, totalChance);
        float cumulativeChance = 0f;

        foreach (PowerUpEvents interactableEvents in _PowerUpEvents)
        {
            cumulativeChance += interactableEvents.DropChance;

            if (rand <= cumulativeChance)
            {
                interactableEvents.PowerUpEvent.Invoke();
                return;
            }
        }
    }

        public void DropItem()
    {
        EnemyHealth.enemyIsDead = false;
        enemyPos = GetComponent<Transform>().transform.GetChild(0).GetChild(0).transform;
        Instantiate(itemList[itemIndex], enemyPos.position, Quaternion.identity);
    }
}

[System.Serializable]

public class PowerUpEvents
{
    public string EventName;
    [Space]
    [Space]
    [Range(0f, 1f)] public float DropChance = 0.4f;
    public UnityEvent PowerUpEvent;
}