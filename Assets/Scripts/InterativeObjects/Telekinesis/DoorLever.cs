using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLever : MonoBehaviour, ITelekinesisObject
{
    private Player player;
    private TelekinesisAbility ability;
    Outline outline;
    public float PullSpeed = 1;
    private float pullAcum = 0;
    private Quaternion initRot;
    private Transform lever;
    private Renderer rend;
    public GameObject Door;

    public Vector3 GetPosition()
    {
        return transform.GetChild(0).position;
    }

    public Vector3 GetSelectionPosition()
    {
        return new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
    }

    public void Grab(TelekinesisAbility ability)
    {
        this.ability = ability;
    }

    public void Highlight(bool IsActive)
    {
        outline.enabled = IsActive;
    }

    public void Move(Vector3 offset)
    {
        var projected = Vector3.Project(offset, transform.forward);
        if (projected.normalized == transform.forward.normalized)
        {
            pullAcum += projected.magnitude * PullSpeed;
        }
        else
        {
            pullAcum -= projected.magnitude * PullSpeed;
        }
        if (pullAcum < 0)
            pullAcum = 0;
    }

    public void Release()
    {

    }

    public void Rotate(Vector3 direction, float degrees)
    {

    }

    public void SetPosition(Vector3 position)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        lever = transform.GetChild(0);
        initRot = lever.transform.rotation;
        rend = lever.GetChild(0).GetComponent<Renderer>();
        outline = GetComponentInChildren<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
        outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
        outline.OutlineColor = player.TelekinesisColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (pullAcum < 1)
        {
            lever.transform.rotation = Quaternion.AngleAxis(pullAcum * 90, transform.right) * initRot;
        }
        else
        {
            lever.transform.rotation = Quaternion.AngleAxis(90, transform.right) * initRot;
            ability.Release();
            outline.enabled = false;
            enabled = false;
            Destroy(Door);
        }
    }
}
