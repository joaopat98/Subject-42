using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{   
    public int Speed;

    void Update()
    {
        transform.Rotate(Vector3.up * Speed * Time.deltaTime);
    }
}
