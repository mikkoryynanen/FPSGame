using UnityEngine;

public class RangeEnemy : Enemy
{
    [SerializeField] Transform muzzleTransform;
    [SerializeField] GameObject projectilePrefab;

    float _shootTimer = 0f;

    private void Start()
    {
        _navAgent.stoppingDistance = 10;
    }

    private void Update() 
    {
        if(_shootTimer >= 1f)
        {
            GameObject go = Instantiate(projectilePrefab, muzzleTransform.position, muzzleTransform.rotation);

            _shootTimer = 0f;
        }
        else
        {
            _shootTimer += Time.deltaTime;            
        }
    }
}