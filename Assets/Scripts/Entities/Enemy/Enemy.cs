using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    protected Transform target;    
    protected NavMeshAgent _navAgent;


    public override void Awake()
    {
        base.Awake();

        _navAgent = GetComponent<NavMeshAgent>();
        _navAgent.stoppingDistance = Random.Range(1, 10);

        target = GameObject.Find("Player").transform;
    }

    private void FixedUpdate() 
    {
        if(target == null)
            return;

        _navAgent.SetDestination(target.position);
        transform.LookAt(target);
    }    
}