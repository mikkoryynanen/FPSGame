using UnityEngine;

public class Projectile : MonoBehaviour 
{
    [SerializeField] float speed = 100f;


    private void Update() 
    {   
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) 
    {
        Destroy(this.gameObject);
    }
}