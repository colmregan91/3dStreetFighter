using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerUI : MonoBehaviour
{
    private TextMeshProUGUI tmPro;
    private void Awake()
    {
        tmPro = GetComponent<TextMeshProUGUI>();
    }
    public void HandlePlayerInitialized()
    {
        tmPro.text = "Player Joined!";
        StartCoroutine(ClearTxtAfterDelay());

    }

    private IEnumerator ClearTxtAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        tmPro.text = string.Empty;
    }
}
