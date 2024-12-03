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
        if (collision.collider.gameObject.tag == "Enemy") 
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(weapon.damage);
        }
    }
}
