using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterSelectionPanel : MonoBehaviour
{
    [SerializeField] private character characterPrefab;

    public UICharacterSelectionPanel rightPanel;
    public UICharacterSelectionPanel leftpanel;
public character CharacterPrefab { get { return characterPrefab; } }
}
