using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JoystickUtils;
using System.Linq;

public class TelekinesisAbility : Ability
{
    ITelekinesisObject currentObj = null;
    LineRenderer line;

    List<ITelekinesisObject> GetTelekinesisObjects()
    {
        return GameObject.FindObjectsOfType<MonoBehaviour>().Where(obj => obj.enabled).OfType<ITelekinesisObject>().ToList();
    }
    // Start is called before the first frame update
    public TelekinesisAbility(Player player) : base(player)
    {
        line = player.GetComponent<LineRenderer>();
    }

    ITelekinesisObject GetClosestObject()
    {

        ITelekinesisObject currentClosestObject = null;
        float currentLowestAngle = Mathf.Infinity;

        foreach (ITelekinesisObject obj in GetTelekinesisObjects())
        {

            if (Vector3.Angle(player.transform.forward, obj.GetSelectionPosition() - player.transform.position) < currentLowestAngle
                && Vector3.Distance(player.transform.position, obj.GetSelectionPosition()) <= player.ViewRange
                && Vector3.Angle(player.transform.forward, obj.GetSelectionPosition() - player.transform.position) < player.ViewAngle)
            {
                currentClosestObject = obj;
                currentLowestAngle = (Vector3.Angle(player.transform.forward, obj.GetSelectionPosition() - player.transform.position));
            }
        }
        return currentClosestObject;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (currentObj == null)
        {
            var currentClosestObject = GetClosestObject();
            foreach (ITelekinesisObject obj in GetTelekinesisObjects())
            {
                obj.Highlight(obj == currentClosestObject);
            }
            if ((Input.GetButtonDown("Power") || Input.GetMouseButtonDown(0)) && currentClosestObject != null)
            {
                currentObj = currentClosestObject;
                currentObj.Grab(this);
                line.enabled = true;
                line.SetPosition(0, player.transform.position);
                line.SetPosition(1, currentObj.GetPosition());
            }
        }
        else
        {
            if (Input.GetAxisRaw("DPad Y") < 0 || Input.GetKey(KeyCode.Space))
            {
                var dir = Joystick.GetJoystick2Dir().ToHorizontalDir().CameraCorrect();

                if (dir == Vector3.zero)
                {
                    if(Input.GetKey(KeyCode.LeftArrow))
                        dir.z += 1;

                    if(Input.GetKey(KeyCode.RightArrow))
                        dir.z += -1;

                    if(Input.GetKey(KeyCode.UpArrow))
                        dir.x += 1;

                    if(Input.GetKey(KeyCode.DownArrow))
                        dir.x += -1;
                }

                currentObj.Rotate(dir, player.TelekinesisRotateSpeed * Time.deltaTime);
            }
            else
            {
                var offset = Joystick.GetJoystick2Dir().ToHorizontalDir().CameraCorrect();

                if (offset == Vector3.zero)
                {
                    if(Input.GetKey(KeyCode.LeftArrow))
                        offset.z += 1;

                    if(Input.GetKey(KeyCode.RightArrow))
                        offset.z += -1;

                    if(Input.GetKey(KeyCode.UpArrow))
                        offset.x += 1;

                    if(Input.GetKey(KeyCode.DownArrow))
                        offset.x += -1;
                }

                offset = offset * player.TelekinesisMoveSpeed * Time.deltaTime;

                currentObj.Move(offset);
            }
            line.SetPosition(0, player.transform.position);
            line.SetPosition(1, currentObj.GetPosition());
            if ((Input.GetButtonDown("Power") || Input.GetMouseButtonDown(0))
                || Vector3.Distance(player.transform.position, currentObj.GetSelectionPosition()) > player.TelekinesisRange)
            {
                Release();
            }
        }
    }

    public override void SwitchAbility(int delta)
    {
        foreach (ITelekinesisObject obj in GetTelekinesisObjects())
        {
            obj.Highlight(false);
        }
        line.enabled = false;
        if (currentObj != null)
        {
            currentObj.Release();
            currentObj = null;
        }
        base.SwitchAbility(delta);
    }

    public void Release()
    {
        line.enabled = false;
        if (currentObj != null)
        {
            currentObj.Release();
            currentObj = null;
        }
    }
}
