using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class PlayerManager : MonoBehaviour
{
    private Player[] Players;
    

    private void Start()
    {
        Players = GetComponentsInChildren<Player>();

        GameStateMachine.instance.HandleBeginGame += SpawnPLayerCharacters;
        GameStateMachine.instance.HandleAddingPlayersToGame += AddPlayerToGame;

        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        GameStateMachine.instance.HandleBeginGame -= SpawnPLayerCharacters;
        GameStateMachine.instance.HandleAddingPlayersToGame -= AddPlayerToGame;
    }
    public void AddPlayerToGame(IControlPlayers cont)
    {
        var firstUnassignedPlayer = Players.OrderBy (T => T.playerNumber).FirstOrDefault(T => !T.hasController);
        firstUnassignedPlayer.InitializePlayer(cont);
    }

    public void SpawnPLayerCharacters()
    {
        foreach (var player in Players)
        {
            if (player.hasController && player.Character != null)
            {

                player.SpawnCharacter();
         
            }
        }
    }
}
