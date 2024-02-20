using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Objective : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;
    public LayerMask isGround, isPlayer;
    BoxCollider boxCollider;
    
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    

    public float sightRange;
    public bool playerInSightRange;

    private void Awake()
    {
        player = GameObject.Find("player");
        agent = GetComponent<NavMeshAgent>();
        boxCollider = GetComponentInChildren<BoxCollider>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);

        if (playerInSightRange)
        {
            Escape();// chase player if in sight
        }else{
            Patrolling();// Patrol if not in sight
        }
    }

    private void Patrolling()
    {
        if(!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position -walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        // For it to go a random direction

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);//setting that direction
        if (Physics.Raycast(walkPoint, Vector3.down, isGround))
        {
            walkPointSet = true;
        }
        //Making sure it is on the ground
    }

    private void Escape()
    {
        Vector3 fleeVector = -1*(player.transform.position - this.transform.position);//Flee behavior
        agent.SetDestination(this.transform.position + fleeVector);//Escape the player
    }

    private void OnTriggerEnter(Collider other)
    {
        var playerMov = other.GetComponent<PlayerMotor>();
        if(playerMov != null)
        {
            Debug.Log("Catch");
        }
    }
}
