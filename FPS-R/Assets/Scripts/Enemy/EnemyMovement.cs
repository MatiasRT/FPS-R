using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] float distToChase = 50;
    private Transform player;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private NavMeshAgent nav;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        if (enemyHealth.CurrentHealth > 0 && playerHealth.CurrentHealth > 0)
        {
            Invoke("SetDestiny", 1f);
        }
        else
        {
            nav.enabled = false;
        }
    }

    void SetDestiny()
    {
        float dist = Vector3.Distance(player.position, transform.position);
        if (nav.enabled && (dist <= distToChase))
        {
            nav.SetDestination(player.position);
            Invoke("SetDestiny", 5f);
        }
    }
}
