using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform playert;
    public Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playert.position.z < this.transform.position.z)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - Time.deltaTime);
        }

        if (playert.position.z > this.transform.position.z)
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + Time.deltaTime);
        }

        if (playert.position.x < this.transform.position.x)
        {
            this.transform.position = new Vector3(this.transform.position.x - Time.deltaTime, this.transform.position.y, this.transform.position.z);
        }

        if (playert.position.x > this.transform.position.x)
        {
            this.transform.position = new Vector3(this.transform.position.x + Time.deltaTime, this.transform.position.y, this.transform.position.z);
        }
    }
}
