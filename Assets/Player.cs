using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var dirRaw = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float angle = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        float absAngle = Mathf.Atan2(Mathf.Abs(Input.GetAxis("Vertical")), Mathf.Abs(Input.GetAxis("Horizontal")));
        Debug.Log(Mathf.Rad2Deg * absAngle);
        absAngle = absAngle > Mathf.PI / 4 ? Mathf.PI / 2 - absAngle : absAngle;
        float ratio = absAngle == 0 || absAngle == 90 ? 1 : Mathf.Sin(absAngle) / Mathf.Tan(absAngle);
        var dir = ratio * dirRaw;
        Debug.Log(dir);
        rb.MoveRotation(Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.up));
        rb.velocity = new Vector3(dir.x, 0, dir.y) * Speed;
    }
}
