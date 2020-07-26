using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIhealthBar : MonoBehaviour
{
    [SerializeField] private Image foregroundImage;
    private character CurrentCharacter;
    private Player player;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
        player.OnPlayerChanged += PlayerChanged;
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        player.OnPlayerChanged -= PlayerChanged;
    }

    void PlayerChanged(character character)
    {
        CurrentCharacter = character;
        CurrentCharacter.OnHealthChanged += OnHealthChanged;
        CurrentCharacter.OnDied += OnDied;    
       gameObject.SetActive(true);
    }

    private void OnDied(IDie Entity)
    {
        Entity.OnHealthChanged -= OnHealthChanged;
        Entity.OnDied -= OnDied;
        CurrentCharacter = null;
        gameObject.SetActive(false);
    }

    private void OnHealthChanged(int currentHealth, int MaxHealth)
    {
        float pct = (float)currentHealth / MaxHealth;
        foregroundImage.fillAmount = pct;
    }
}
