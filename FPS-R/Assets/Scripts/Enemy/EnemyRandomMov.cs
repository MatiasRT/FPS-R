using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRandomMov : MonoBehaviour {

    [SerializeField] float wanderRadius;
    [SerializeField] float wanderTimer;
    

    private Transform target;
    private NavMeshAgent agent;
    Animator anim;
    private float timer;
    float speed = 0f;
    bool isMoving;

    // Use this for initialization
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update()
    {
        isMoving = false;
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
       
            timer = 0; 
        }
        if (Vector3.Distance(transform.position, agent.destination) > 1.1f)
            speed = Mathf.Lerp(speed, 3.5f, 0.05f);
        else
            speed = Mathf.Lerp(speed, 0f, 0.05f);
        

        anim.SetFloat("Blend", speed);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position; 
    }
}
