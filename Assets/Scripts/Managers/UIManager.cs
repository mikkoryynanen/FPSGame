using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text ammoCounter;
    [SerializeField] HotBar hotBar;

    [SerializeField] Weapon[] currentWeapons;

    private void Awake() 
    {
        hotBar.Build(currentWeapons);
    }

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
