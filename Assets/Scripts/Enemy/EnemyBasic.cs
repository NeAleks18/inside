using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBasic : MonoBehaviour
{
    // ИИ Параметры
    private NavMeshAgent agent;
    private GameObject player;
    
    // Харастеристики
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private float stoppingDistance = 2f;
    [SerializeField] private float attackInterval = 0.5f;
    public short HPEnemy = 3;
    public short dmg = 1;
    
    private float Dist_Player;
    private float attackTimer;
    private Vector3 OldPosition;
    [SerializeField] private Animator Animator;
    [SerializeField] private ParticleSystem damageGet;
    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        agent = GetComponent<NavMeshAgent>();
        
    }
    
    private void MovementAI()
    {
        agent.SetDestination(player.transform.position);
        transform.LookAt(player.transform);
        agent.stoppingDistance = stoppingDistance;
    }

    private void Checker()
    {

        // Проверка на движение игрока
        if (OldPosition != transform.position)
            Animator.SetBool("walk", true);
        else
            Animator.SetBool("walk", false);
        OldPosition = transform.position;
    }
    
    private void Update()
    {
        Dist_Player = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if (Dist_Player <= detectionRadius)
        {
            MovementAI();
            if (attackTimer <= 0f)
            {
                Attack();
                attackTimer = attackInterval;
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
        }
        Checker();
    }
    
    private void Attack()
    {
        // Attack logic here
        if (Dist_Player >= 2f) return;
        //Debug.Log("АТАКА!");
        ParticleSystem damageInstance = Instantiate(damageGet, new Vector3(player.transform.position.x, player.transform.position.y + 0.693f, player.transform.position.z), Quaternion.identity);
        damageInstance.transform.SetParent(player.transform);
        player.GetComponent<PlayerPawn>().Health -= dmg;
    }
}
