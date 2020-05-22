using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 5;
    public float Range = 20;
    public Color color;
    private float travelled = 0;
    private Vector3 prevPos;


    void OnTriggerEnter(Collider col)
    {
        // Kill player if bullet hits
        if (col.CompareTag("Player"))
        {
            col.GetComponent<Player>().Kill();
            Destroy(gameObject);
        }
        // Ignore collision if it was with guard, otherwise destroy the bullet
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
        GlowBullet();

    }

    void GlowBullet()
    {
        foreach (var mat in GetComponentInChildren<MeshRenderer>().materials)
        {
            if (mat.HasProperty("GlowColor") && mat.HasProperty("GlowIntensity"))
            {
                mat.SetFloat("GlowIntensity", 1);
                mat.SetColor("GlowColor", color);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the bullet has travelled for long enough
        travelled += Vector3.Distance(transform.position, prevPos);
        prevPos = transform.position;
        if (travelled > Range)
        {
            Destroy(gameObject);
        }
    }
}
