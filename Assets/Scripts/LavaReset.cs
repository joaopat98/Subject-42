using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaReset : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject Player;
    public GameObject PlayerOriginalPosition;

    List<Vector3> CubesPositions;
    GameObject[] Cubes;
    void Start()
    {
        Cubes = GameObject.FindGameObjectsWithTag("TelekinesisObject");
        CubesPositions = new List<Vector3>();
        for(int i = 0; i < Cubes.Length; i++)
        {
            CubesPositions.Add(Cubes[i].transform.position);
        }
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            Player.transform.position = PlayerOriginalPosition.transform.position;
            for (int i = 0; i < Cubes.Length; i++)
            {
                Cubes[i].transform.position = CubesPositions[i];
            }
        }
    }
}
