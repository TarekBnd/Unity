using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyManager enemyManager;
    public float enemyHealth = 4f;
    public float enemyDamage = 20f;
    


    public GameObject animEnemyDeath;
    public GameObject gunHitEffect;
    public GameObject projectile;


    public bool isBat;
    public bool isGhost;
    private bool alreadyAttacked;

    public float timebetweenAttacks;

    public LayerMask whatIsGround, WhatIsPlayer;

    public PlayerMove player;
    private Transform playersTransform;
    private UnityEngine.AI.NavMeshAgent enemyNavMeshAgent;

    void Start()
    {
        playersTransform = FindObjectOfType<PlayerMove>().transform;
        enemyNavMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    
    void SearchPlayer()
    {

        if(player.isInvisible)
        {
            enemyNavMeshAgent.SetDestination(transform.position); 
        } 
        else 
        {
            enemyNavMeshAgent.SetDestination(playersTransform.position); 
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void Update()
    {
        SearchPlayer();
    }

    public void TakeDamage(float damage)
    {
        Instantiate(gunHitEffect, transform.position, Quaternion.identity);
        enemyHealth -= damage;
        if(enemyHealth <= 0)
        {
            enemyManager.RemoveEnemy(this);
            Destroy(gameObject);
            Instantiate(animEnemyDeath, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().DamagePlayer((int)enemyDamage);
        }
    }
}
