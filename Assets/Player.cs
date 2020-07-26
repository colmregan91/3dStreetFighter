using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Player : MonoBehaviour
{
    public event Action<character> OnPlayerChanged;
    private PlayerUI playerUI;
    public int playerNumber;
    public PooledMonoBehavior ImpactParticles; // REMOVE
    public PooledMonoBehavior DeathParticles; // REMOVE
    public IControlPlayers controller { get; private set; }
    public bool hasController { get { return controller != null; } }

    public character Character;
    private static int SeparateInt = 1;

    private void Start()
    {

        playerUI = GetComponentInChildren<PlayerUI>();
    }

    public void InitializePlayer (IControlPlayers cont)
    {
        this.controller = cont;
        gameObject.name = "Player - " + playerNumber;
        playerUI.HandlePlayerInitialized();

    }

    public void SpawnCharacter()
    {
        SeparateInt = SeparateInt * -1;
        Vector3 pos = new Vector3(SeparateInt, 0, 0);
        var character = Character.Get <character>(new Vector3 (SeparateInt, 0, 0), Quaternion.identity);
        character.SetController (controller);
        character.OnDied += CharacterDied;
        OnPlayerChanged?.Invoke(character);
    }

    private void CharacterDied(IDie entity)
    {
        entity.OnDied -= CharacterDied;
       
        Invoke("respawn", 4f);
    }
    private void respawn()
    {
        SpawnCharacter();
    }
}
