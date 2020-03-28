using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    /// <summary>
    /// Rigidbody attached to this GameObject
    /// </summary>
    Rigidbody rb;
    /// <summary>
    /// Speed at which the player shall move
    /// </summary>
    public float MoveSpeed;
    public Material DeadMaterial;

    public bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
            Move();
    }

    /// <summary>
    /// Get direction associated with the Left Joystick (converts from square coordinates to circle)
    /// </summary>
    private Vector2 GetJoystickDir()
    {
        var dirRaw = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        float angle = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        float absAngle = Mathf.Atan2(Mathf.Abs(Input.GetAxis("Vertical")), Mathf.Abs(Input.GetAxis("Horizontal")));
        absAngle = absAngle > Mathf.PI / 4 ? Mathf.PI / 2 - absAngle : absAngle;
        float ratio = absAngle == 0 || absAngle == 90 ? 1 : Mathf.Sin(absAngle) / Mathf.Tan(absAngle);
        return ratio * dirRaw;
    }

    /// <summary>
    /// Routine to move the player;
    /// </summary>
    private void Move()
    {
        // Get the camera angle relative to the world z axis
        var camTransform = Camera.main.transform;
        var camAngle = Mathf.Rad2Deg * Mathf.Atan2(camTransform.forward.x, camTransform.forward.z);

        // Apply camera angle to the movement direction, making movement relative to the camera
        var dir = Quaternion.Euler(0, camAngle, 0) * GetJoystickDir().ToHorizontalDir();

        // Rotate player towards the direction it is moving in
        if (dir != Vector3.zero)
            rb.MoveRotation(Quaternion.LookRotation(dir, Vector3.up));
        else
            rb.angularVelocity = Vector3.zero;
        rb.velocity = new Vector3(MoveSpeed * dir.x, rb.velocity.y, MoveSpeed * dir.z);
    }

    /// <summary>
    /// Disable input and schedule level restart
    /// </summary>
    public void Kill()
    {
        isAlive = false;
        GetComponent<Renderer>().material = DeadMaterial;
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
