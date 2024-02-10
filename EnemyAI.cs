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

    public float timeBetweenAttack;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("player")
        agent = GetComponent<NavMeshAgent>();
        boxCollider = GetComponentInChildren<BoxCollider>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);

        if (!playerInAttackRange && !playerInSightRange)
        {
            Patrolling();
        }
        if (playerInSightRange && !playerInAttackRange)
        {
            Chase();
        }
    }

    private void Patrolling()
    {
        if(!walkPointSet)
        {
            SearchWalkPoint();
            if (walkPointSet)
            {
                agent.SetDestination(walkPoint);
            }
            Vector3 distanceToWalkPoint = transform.position -walkPoint;
            if (distanceToWalkPoint.magnitude < 0f)
            {
                walkPointSet = false;
            }
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        // For it to go a random direction

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);//setting that direction
        if (Physics.Raycast(walkPoint, -transform.up, 2f, isGround))
        {
            walkPointSet = true;
        }
        //Making sure it is on the ground
    }

    private void Chase()
    {
        agent.SetDestination(player.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        var playerMov = other.GetComponent<PlayerMotor>();
        if(playerMov != null)
        {
            GameManager.EndGame();
        }
    }
}
