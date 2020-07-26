using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    private IControlPlayers[] controllers;
    private void Awake()
    {
        controllers = GetComponentsInChildren<IControlPlayers>();
        int index = 1;
        foreach (var cont in controllers)
        {
            cont.SetIndex(index);
            index++;

        }


    }

    private void Update()
    {
        foreach (var cont in controllers)
        {
            if (!cont.isassigned && cont.anyButtonDown())
            {
                assignController(cont);
                GameStateMachine.instance.AddAPlayer(cont);
            }
        }
    }

    private void assignController(IControlPlayers cont)
    {
        cont.isassigned = true;
    }
}
