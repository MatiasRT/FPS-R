using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    [SerializeField] int startingHealth = 100;
    [SerializeField] int currentHealth;
    //[SerializeField] Slider healthSlider;
    //[SerializeField] Image damageImage;
    [SerializeField] AudioClip deathClip;
    [SerializeField] float flashSpeed = 5f;
    [SerializeField] Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    private Animator anim;
    private AudioSource playerAudio;
    private PlayerMove playerMovement;
    private Weapon playerShooting;
    private bool isDead;
    private bool damaged;

    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMove>();
        playerShooting = GetComponentInChildren<Weapon>();
        currentHealth = startingHealth;
    }


    void Update()
    {
        if (damaged)
        {
            //damageImage.color = flashColour;
        }
        else
        {
            //damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void TakeDamage(int amount)
    {
        damaged = true;

        currentHealth -= amount;

        //healthSlider.value = currentHealth;

        playerAudio.Play();

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;

        //playerShooting.DisableEffects();

        //anim.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }


    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
