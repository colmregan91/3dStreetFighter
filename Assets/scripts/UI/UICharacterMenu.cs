using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterMenu : MonoBehaviour
{
    [SerializeField] private UICharacterSelectionPanel leftPanel;
    [SerializeField] private UICharacterSelectionPanel middlePanel;
    [SerializeField] private UICharacterSelectionPanel rightPanel;

    [HideInInspector]public UICharacterSelectionPanel[] panels;

    public UICharacterSelectionPanel LeftPanel { get { return leftPanel; } }
        public UICharacterSelectionPanel MiddlePanel { get { return middlePanel; } }
    public UICharacterSelectionPanel RightPanel { get { return rightPanel; } }

    public GameObject startGameTxt;

    private PlayerSelectionMarker[] markers;
    private bool startEnabled;

    private void Awake()
    {
        //panels[0] = LeftPanel;
        //panels[1] = MiddlePanel;
        //panels[2] = RightPanel;
        startGameTxt.SetActive(false);
        markers = GetComponentsInChildren<PlayerSelectionMarker>();
    }

    private void Update()
    {
        int playersIn = 0;
        int playersLockedIn = 0;
        foreach (var marker in markers)
        {
            if (marker.isPlayerIn)
            {
                playersIn++;
            }
            if (marker.isLockedIn)
            {
                playersLockedIn++;
            }
        }

        startEnabled = playersIn > 0 && playersIn == playersLockedIn;
        startGameTxt.SetActive(startEnabled);
    }

    internal void startGame()
    {
       if (startEnabled)
        {
            GameStateMachine.instance.LoadingSceneLoad();
            gameObject.SetActive(false);
        }
    }
}
