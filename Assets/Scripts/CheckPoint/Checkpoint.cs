using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    /// <summary>
    /// Position to put the player
    /// </summary>
    public Transform checkpoint;
    GameObject player;

    /// <summary>
    /// Object to reset the position and rotation
    /// </summary>
    public GameObject[] objects;

    /// <summary>
    /// Checkpoint position of the object
    /// </summary>
    List<Vector3> objResetPos;

    /// <summary>
    /// Checkpoint rotation of the object
    /// </summary>
    List<Vector3> objResetRot;
    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.objResetPos = new List<Vector3>();
        this.objResetRot = new List<Vector3>();
        ObtainOriginalPosition();

    }

    /// <summary>
    /// Fill the rotation and position list with the checkpoint position
    /// </summary>
    void ObtainOriginalPosition()
    {
        for(int i = 0; i < objects.Length; i++)
        {
            this.objResetPos.Add(objects[i].transform.position); 
            this.objResetRot.Add(objects[i].transform.rotation.eulerAngles);
        }
    }

    /// <summary>
    /// Reset the objects
    /// </summary>
    void ResetToCheckpoint()
    {
        player.transform.position = checkpoint.position;
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].transform.position = objResetPos[i];
            objects[i].transform.rotation = Quaternion.Euler(objResetRot[i]);
           
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            ResetToCheckpoint();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ResetToCheckpoint();
        }
    }

}
