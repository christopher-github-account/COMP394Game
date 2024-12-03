using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float timeToDestroy;
    [HideInInspector] public WeaponManager weapon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*// Take Damage Code Here
         * 
         * 
         * zombie.TakeDamage(weapon.damage); <<<---------
         * 
         * 
        if (collision.gameObject.GetComponentInParent<EnemyHealth>()) 
        {
            EnemyHealth enemyHealth = gameObject.GetComponentInParent<EnemyHealth>();
            enemyHealth.TakeDamage(weapon.damage);
        }//*/
        Destroy(this.gameObject);
    }
}
