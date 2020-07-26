using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectionMarker : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Image LockImage;
    [SerializeField] private Image SelectionToggleImage;

    private UICharacterSelectionPanel currentPAnel;
    private UICharacterMenu SelectionMenu;
    private bool initialising;
    private bool initialized;

    public bool isLockedIn { get; private set; }
    public bool isPlayerIn { get { return player.hasController; } }

    private void Awake()
    {
   
        SelectionMenu = GetComponentInParent<UICharacterMenu>();
        LockImage.gameObject.SetActive(false);
        SelectionToggleImage.gameObject.SetActive(false);
        MoveToPositionAndSelect(SelectionMenu.MiddlePanel);
    }

    private void Update()
    {
        if (!isPlayerIn) return;

        if (!initialising)
        {
            StartCoroutine(initialize());
        }
        if (!initialized) return;

        if (player.controller.X_Button())
        {
            lockPlayer();
        }

        if (!isLockedIn)
        {
            if (player.controller.Right_Button())
            {
                UICharacterSelectionPanel nextPanel = currentPAnel.rightPanel;
                MoveToPositionAndSelect(nextPanel);

            }
            else if (player.controller.Left_Button())
            {
                UICharacterSelectionPanel nextPanel = currentPAnel.leftpanel;
                MoveToPositionAndSelect(nextPanel);
            }
        }
        else
        {
            if (player.controller.Start_Button())
            {
                SelectionMenu.startGame();
            }

            if (player.controller.Circle_Button())
            {
                UnlockPlayer();
            }
        }

 
    }

    private void lockPlayer()
    {
        LockImage.gameObject.SetActive(true);
        isLockedIn = true;
    }
    private void UnlockPlayer()
    {
        LockImage.gameObject.SetActive(false);
        isLockedIn = false;
    }

    private void MoveToPositionAndSelect(UICharacterSelectionPanel panel)
    {
        transform.position = panel.transform.position;
        currentPAnel = panel;
        player.Character = panel.CharacterPrefab;
    }


    private IEnumerator initialize()
    {
        initialising = true;
        yield return new WaitForSeconds(0.5f);
        SelectionToggleImage.gameObject.SetActive(true);
        initialized = true;
    }
}
