using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text ammoCounter;

    private void OnEnable() 
    {
        EventManager.Subscribe<OnFireWeapon>(OnUpdateUI);
    }

    private void OnDisable() 
    {
        EventManager.Unsubscribe<OnFireWeapon>();
    }

    private void OnUpdateUI(OnFireWeapon ev)
    {
        ammoCounter.text = $"{ev.currentAmmo} / {ev.maxAmmo}";
    }
}
