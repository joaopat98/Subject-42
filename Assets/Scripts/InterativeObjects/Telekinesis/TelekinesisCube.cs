using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelekinesisCube : MonoBehaviour, ITelekinesisObject
{
    Vector3 prevPlayerPos;
    Vector3 velocity;
    Player player;
    Rigidbody rb;
    public Material DefaultMaterial, SelectedMaterial;
    public float MoveSpeed = 1;
    public float RotateSpeed = 180;
    public float VerticalOffset = 0.5f;
    public float RiseTime = 0.5f;
    public float MinDistPlayer = 2;
    public float MaxDistPlayer = 6.5f;
    public float RepelForce = 0.1f;
    bool moved = false;

    bool isHeld;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;
        if (isHeld)
        {
            Move(playerPos - prevPlayerPos);
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            var playerOffset = GetPosition() - player.transform.position;
            if (moved && playerOffset.magnitude < MinDistPlayer)
            {
                playerOffset.y = 0;
                rb.AddForce(playerOffset.normalized * RepelForce * Time.deltaTime, ForceMode.VelocityChange);
            }
        }
        prevPlayerPos = playerPos;
    }

    public void Grab(TelekinesisAbility ability)
    {
        GetComponent<Renderer>().material = DefaultMaterial;
        StartCoroutine(MoveToStart());
        isHeld = true;
        rb.useGravity = false;
    }

    public void Release()
    {
        isHeld = false;
        moved = false;
        rb.useGravity = true;
        rb.velocity = velocity;
    }

    public void Highlight(bool isActive)
    {
        if (isActive)
        {
            GetComponent<Renderer>().material = SelectedMaterial;
        }
        else
        {
            GetComponent<Renderer>().material = DefaultMaterial;
        }
    }

    public void Move(Vector3 offset)
    {
        velocity = offset / Time.deltaTime + player.GetComponent<Rigidbody>().velocity;
        var playerOffset = GetPosition() - player.transform.position;
        var nextPlayerOffset = GetPosition() + offset - player.transform.position;
        var height = Vector3.up * playerOffset.y;
        playerOffset -= height;
        nextPlayerOffset -= height;
        if (
            (nextPlayerOffset.magnitude < MaxDistPlayer
            || nextPlayerOffset.magnitude < playerOffset.magnitude)
            && (nextPlayerOffset.magnitude > MinDistPlayer
            || nextPlayerOffset.magnitude > playerOffset.magnitude))
        {
            SetPosition(player.transform.position + nextPlayerOffset + height);
        }
        else
        {
            SetPosition(player.transform.position + (playerOffset.magnitude * nextPlayerOffset.normalized) + height);
        }
    }

    public void SetPosition(Vector3 position)
    {
        rb.MovePosition(position);
    }

    public void Rotate(Vector3 direction, float degrees)
    {
        var rotAxis = Quaternion.Euler(0, 90, 0) * direction;
        rb.MoveRotation(Quaternion.AngleAxis(degrees, rotAxis) * rb.rotation);
    }

    public Vector3 GetSelectionPosition()
    {
        return transform.position;
    }

    public Vector3 GetPosition()
    {
        return rb.position;
    }

    IEnumerator MoveToStart()
    {
        float t = 0;
        float prevT = 0;
        Vector3 fromOffset = rb.position - player.transform.position;
        Vector3 toOffset;
        if (fromOffset.magnitude < player.TelekinesisMoveClose)
        {
            toOffset = player.transform.position + fromOffset.normalized * (player.TelekinesisMoveClose + 0.1f);
        }
        else
        {
            toOffset = player.transform.position + fromOffset;
        }
        toOffset += VerticalOffset * Vector3.up - rb.position;
        while (t < RiseTime)
        {
            t += Time.deltaTime;
            Vector3 offset = EasingFunction.EaseOutCubic(0, 1, t) * toOffset;
            offset -= EasingFunction.EaseOutCubic(0, 1, prevT) * toOffset;
            rb.MovePosition(rb.position + offset);
            prevT = t;
            yield return 0;
        }
        moved = true;
    }

    public bool IsActive()
    {
        return enabled;
    }
}
