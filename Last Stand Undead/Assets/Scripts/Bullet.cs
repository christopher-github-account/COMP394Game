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
        Debug.Log("Hit");
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy");
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(weapon.damage);
        }
        Destroy(gameObject);
    }
}
