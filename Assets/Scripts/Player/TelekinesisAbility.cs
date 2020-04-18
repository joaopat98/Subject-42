﻿using System.Collections;
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
            if (Input.GetButtonDown("Power") && currentClosestObject != null)
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
            if (Input.GetAxisRaw("DPad Y") < 0)
            {
                currentObj.Rotate(
                    Joystick.GetJoystick2Dir().ToHorizontalDir().CameraCorrect(),
                    player.TelekinesisRotateSpeed * Time.deltaTime
                    );
            }
            else
            {
                var offset = Joystick.GetJoystick2Dir().ToHorizontalDir().CameraCorrect() * player.TelekinesisMoveSpeed * Time.deltaTime;
                currentObj.Move(offset);
            }
            line.SetPosition(0, player.transform.position);
            line.SetPosition(1, currentObj.GetPosition());
            if (Input.GetButtonDown("Power")
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