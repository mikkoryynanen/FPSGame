using UnityEngine;

public class HotBar : MonoBehaviour 
{
    HotBarItem[] _hotBarItems;

    private void Awake() 
    {
        _hotBarItems = GetComponentsInChildren<HotBarItem>();
    }

    public void Build(Weapon[] currentWeapons)
    {
        for (int i = 0; i < _hotBarItems.Length; i++)
        {
            Weapon w = i >= currentWeapons.Length ? null : currentWeapons[i];
            _hotBarItems[i].Build(w, i == 0);
        }
    }
}