using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class playerTracker : MonoBehaviour
{
    private CinemachineTargetGroup targetGroup;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        targetGroup = GetComponent<CinemachineTargetGroup>();
        var players = FindObjectsOfType<Player>();
        foreach (var player in players)
        {
            player.OnPlayerChanged += (Character) => HandlePlayerCamera(Character, player);
        }
    }

    void HandlePlayerCamera(character character, Player player)
    {
        int playernumber = player.playerNumber - 1;
        targetGroup.m_Targets[playernumber].target = character.transform;
    }
}
