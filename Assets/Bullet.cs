using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 5;
    public float Range = 20;
    private float travelled = 0;
    private Vector3 prevPos;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<Player>().Kill();
            Destroy(gameObject);
        }
        else if (!col.CompareTag("Guard"))
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * Speed;
        prevPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        travelled += Vector3.Distance(transform.position, prevPos);
        prevPos = transform.position;
        if (travelled > Range)
        {
            Destroy(gameObject);
        }
    }
}
