using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon", order = 0)]
public class Weapon : ScriptableObject 
{
    public int damage;
    public int clipSize;
    public float fireRate;
    
}