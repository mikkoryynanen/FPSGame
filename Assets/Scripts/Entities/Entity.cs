using UnityEngine;

public class Entity : MonoBehaviour, IDamageable 
{

    public int Health { get; private set; }
    public int MaxHealth { get { return 100; } }


    public virtual void Awake() 
    {
        Health = MaxHealth;
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if(Health <= 0)
        {
            EventManager.Send(new OnEnemyKilled { });
            Destroy(this.gameObject);
        }

        // Debug.Log($"Took damage {amount}. Health remaining {Health}");

    }    
}