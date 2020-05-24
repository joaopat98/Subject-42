using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{   
    public int Speed;
    public Vector3 Axis = Vector3.up;
    
    void Update()
    {
        transform.Rotate(Axis * Speed * Time.deltaTime);
    }
}
