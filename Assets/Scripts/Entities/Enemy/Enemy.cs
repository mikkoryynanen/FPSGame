using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity
{
    [SerializeField] Transform target;    

    NavMeshAgent _navAgent;


    public override void Awake()
    {
        base.Awake();
        _navAgent = GetComponent<NavMeshAgent>();

        target = GameObject.Find("Player").transform;
    }

    private void FixedUpdate() 
    {
        if(target == null)
            return;

        _navAgent.SetDestination(target.position);        
    }    
}