using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOverWall : MonoBehaviour, ITelekinesisObject
{
    public float FallThreshold = 3,
    MaxVibration = 0.5f,
    FallRecover = 1,
    FallTime = 1;

    public Material DefaultMaterial, SelectedMaterial;
    float fallAcum = 0;
    float fallTimeAcum = 0;
    bool fallen;
    Vector3 initPos;
    Quaternion initRot;
    Player player;
    TelekinesisAbility ability;

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
        transform.GetChild(0).GetComponent<Renderer>().material = DefaultMaterial;
    }

    public void Highlight(bool IsActive)
    {
        if (IsActive)
        {
            transform.GetChild(0).GetComponent<Renderer>().material = SelectedMaterial;
        }
        else
        {
            transform.GetChild(0).GetComponent<Renderer>().material = DefaultMaterial;
        }
    }

    public void Move(Vector3 offset)
    {
        var projected = Vector3.Project(offset, -transform.forward);
        if (projected.normalized == -transform.forward.normalized)
        {
            fallAcum += projected.magnitude;
        }
        else
        {
            fallAcum -= projected.magnitude;
        }
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
        initPos = transform.position;
        initRot = transform.rotation;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!fallen)
        {
            transform.position = initPos + (fallAcum / FallThreshold) * (
                (transform.up * Random.Range(-MaxVibration, MaxVibration)) +
                (transform.right * Random.Range(-MaxVibration, MaxVibration))
                );
            fallAcum -= FallRecover * Time.deltaTime;
        }
        else
        {
            fallTimeAcum += Time.deltaTime;
            if (fallTimeAcum <= FallTime)
            {
                transform.rotation = Quaternion.AngleAxis(-90 * EasingFunction.EaseInCubic(0, 1, fallTimeAcum / FallTime), transform.right) * initRot;
            }
            else
            {
                transform.rotation = Quaternion.AngleAxis(-90, transform.right) * initRot;
                ability.Release();
                enabled = false;
            }
        }
        if (fallAcum >= FallThreshold)
        {
            fallen = true;
            fallAcum = 0;
            transform.position = initPos;
            transform.GetChild(0).GetComponent<Collider>().enabled = false;
        }
        if (fallAcum < 0) fallAcum = 0;
    }

    public bool IsActive()
    {
        return enabled;
    }
}
