using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    Action CommandsActivation;
    Action CommandsSetEnabled;
    bool _shiftPressed = false;

    void Start()
    {
        CommandsActivation = delegate { };
        CommandsSetEnabled = CommandsAllowedMethod;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            _shiftPressed = true;
        }
        else
        {
            _shiftPressed = false;
        }
        if (_shiftPressed && Input.GetKeyDown(KeyCode.C)) CommandsSetEnabled();
        CommandsActivation();
    }

    void Commands()
    {
        if (Input.GetKeyDown(KeyCode.M))
            Shop.instance.AddMoney(200);
        if (Input.GetKeyDown(KeyCode.N))
            Shop.instance.LoseMoney(100);
    }

    void CommandsAllowedMethod()
    {
        Debug.Log("Commands Allowed");
        CommandsActivation += Commands;
        CommandsSetEnabled = CommandsDisabledMethod;
    }

    void CommandsDisabledMethod()
    {
        Debug.Log("Commands Disabled");
        CommandsActivation -= Commands;
        CommandsSetEnabled = CommandsAllowedMethod;
    }
}
