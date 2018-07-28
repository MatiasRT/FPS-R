using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] int startingHealth = 100;
    [SerializeField] int currentHealth;
    [SerializeField] int scoreValue = 10;
    [SerializeField] AudioClip deathClip;
    [SerializeField] private AudioClip hurt;
    private Animator anim;
    private AudioSource enemyAudio;
    private ParticleSystem hitParticles;
    private CapsuleCollider capsuleCollider;
    private bool isDead;
    private bool isSinking;

    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount)//, Vector3 hitPoint)
    {
        if (isDead)
            return;

        enemyAudio.PlayOneShot(hurt);

        currentHealth -= amount;

        //hitParticles.transform.position = hitPoint;
        //hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death();
        }
    }


    void Death()
    {
        //Debug.Log("Estoy Muerto");

        isDead = true;

        capsuleCollider.isTrigger = true;
        //GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        anim.SetTrigger("Death");
        Destroy(gameObject, 2.2f);

        enemyAudio.clip = deathClip;
        enemyAudio.Play();

        ScoreManager.score += scoreValue;

        //GetComponent<Animator>().enabled = false;
    }
}
