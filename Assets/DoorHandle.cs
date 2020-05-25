using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandle : MonoBehaviour, ITelekinesisObject
{
    private Player player;
    private TelekinesisAbility ability;
    Outline outline;
    public float RotateSpeed = 1;
    private float rotateAcum = 0;
    private Quaternion initRot;
    Animator animator;

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

    }

    public void Release()
    {

    }

    public void Rotate(Vector3 direction, float degrees)
    {
        var projected = Vector3.Project(direction, Vector3.forward);
        if (projected.normalized == Vector3.forward)
        {
            rotateAcum += projected.magnitude * RotateSpeed * Time.deltaTime;
        }
        else
        {
            rotateAcum -= projected.magnitude * RotateSpeed * Time.deltaTime;
        }
        if (rotateAcum < 0)
            rotateAcum = 0;
    }

    public void SetPosition(Vector3 position)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        initRot = transform.rotation;
        outline = GetComponentInChildren<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
        outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
        outline.OutlineColor = player.TelekinesisColor;
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rotateAcum < 0)
            rotateAcum = 0;
        if (rotateAcum < 2)
        {
            transform.rotation = Quaternion.AngleAxis(rotateAcum * 90, Vector3.right) * initRot;
        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(90, Vector3.right) * initRot;
            ability.Release();
            outline.enabled = false;
            animator.SetBool("Open", true);
            enabled = false;
        }
    }
}
