using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask isGround, isPlayer;
    BoxCollider boxCollider;
    
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    
    public int damage;
    public float sightRange;
    public bool playerInSightRange;

    private void Awake()
    {
        player = GameObject.Find("player")
        agent = GetComponent<NavMeshAgent>();
        boxCollider = GetComponentInChildren<BoxCollider>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);

        if (playerInSightRange)
        {
            Chase();// chase player if in sight
        }else{
            Patrolling();// Patroll if not in sigth
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

    private void Chase()
    {
        agent.SetDestination(player.position);// Go towards the player
    }

    private void OnTriggerEnter(Collider other)
    {
        var playerMov = other.GetComponent<PlayerMotor>();
        if(playerMov != null)
        {
            playerMov.TakeDamage(damage);
        }
    }
}
