using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewEnemLocomotion : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Transform myTransform;
    public Transform currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(currentTarget.position);
        myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
