using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon", order = 0)]
public class Weapon : ScriptableObject 
{
    public Sprite icon;
    public int damage;
    public int clipSize;
    public float fireRate;
    
}