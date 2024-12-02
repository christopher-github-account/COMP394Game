using UnityEngine;
using System.Collections;

public class IncreaseDamage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(IncreaseDamageEffect());
    }

    IEnumerator IncreaseDamageEffect()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter3D(Collider collision)
    {
        if(collision.tag == "Player")
        {
            //add increasing of Damage
            Destroy(gameObject);
        }
    }
}
