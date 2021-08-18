using UnityEngine;
using UnityEngine.UI;

public class HotBarItem : MonoBehaviour 
{
    [SerializeField] Image icon;

    public void Build(Weapon weapon, bool isCurrent)
    {
        if (weapon)
        {
            icon.sprite = weapon.icon;            
        }
        else
        {
            icon.enabled = false;
        }

        if(!isCurrent)
            GetComponent<Image>().enabled = false;
    }
}