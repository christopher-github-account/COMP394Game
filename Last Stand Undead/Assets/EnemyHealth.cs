using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public static bool enemyIsDead;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        enemyIsDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
            GameObject.FindGameObjectWithTag("HUD").GetComponent<KillsTracker>().EnemiesKilledCount++; 
            enemyIsDead = true;
    }
}
