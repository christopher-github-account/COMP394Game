using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHeath = 3;
    public int currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currenHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
