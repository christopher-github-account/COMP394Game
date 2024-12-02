using UnityEngine;
using System.Collections;

public class PowerUpIncreaseRunSpeed : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(IncreaseRunSpeed());
    }

    IEnumerator IncreaseRunSpeed()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter3D(Collider collision)
    {
        if(collision.tag == "Player")
        {
            //add increasing of run speed
            Destroy(gameObject);
        }
    }
}
