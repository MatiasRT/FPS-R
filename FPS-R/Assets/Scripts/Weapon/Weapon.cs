using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Weapon : MonoBehaviour {

    private Animator anim;
    private AudioSource _AudioSource;

    [SerializeField] private float range = 100f;                            //Distancia maxima del arma
    [SerializeField] private int bulletsPerMag = 30;                        //Balas en cada cargador
    [SerializeField] private int bulletsLeft = 200;                         //Todas las balas que tengo
    [SerializeField] private int currentBullets;                            //Las balas que tengo actualmente en el cargador

    [SerializeField] private Text ammoText;
    [SerializeField] private Transform shootPoint;                          //El punto de donde salen las balas
    [SerializeField] private GameObject hitParticles;
    [SerializeField] private GameObject bulletImpact;
    //[SerializeField] private GameObject thisWeapon;

    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioClip shootSound;

    [SerializeField] private float fireRate = 0.1f;                         //El delay entre cada disparo
    [SerializeField] private int damage = 20;

    float fireTimer;                                                        //Contador de tiempo para el delay

    private bool isReloading;                                               //Ve si estamos en la animacion de Reload

    private void OnEnable()
    {
        UpdadateAmmoText();
        
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        _AudioSource = GetComponent<AudioSource>();
        currentBullets = bulletsPerMag;
        UpdadateAmmoText();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //ESTO ES SOLO PARA QUE PIERDA SI NO TIENE BALAS
        /*if (bulletsLeft ==  0 && currentBullets == 0)
        {
            StartCoroutine(LoseScene());                                 //Si no tiene balas, se termina el juego
        }*/

        if (Input.GetButton("Fire1"))
        {
            if (currentBullets > 0)
                Fire();                                                     //Ejecuta la funcion fire si es que presionamos/mantenemos el click izquierdo del mouse
            else if(bulletsLeft > 0)
                DoReload();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentBullets < bulletsPerMag && bulletsLeft > 0)
                DoReload();
        }

        if (fireTimer < fireRate)
            fireTimer += Time.deltaTime;                                    //Add into time counter
    }

    private void FixedUpdate()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

        isReloading = info.IsName("Recharge");                                //Si estamos en el Reload state, isReloading va a ser true por defercto

        //if (info.IsName("Fire")) anim.SetBool("Fire", false);             //Si estamos en el Fire State, reseteamos la variable
    }

    private void Fire()
    {                                                                       //Este disparo va a estar hecho con raycast (castea un rayacst y no crea una bala cada vez que dispara)
        if (fireTimer < fireRate || currentBullets <= 0 || isReloading) return; //Esto seria como el verificador que pregunta si podemos disparar, si el timer terminó, y si las balas llegan a 0 que pare.

        RaycastHit hit;                                                     //Aca vamos a guardar los resultados de donde golpea el raycast

        if (Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, range)) //Este es el disparo en sí
        {
            //if (!hit.collider.isTrigger)
            //{
                //Debug.Log(hit.transform.name + " found!");                      //Muestra el nombre del objeto al que golpea el raycast

                //Esto crea un chispaso justo donde pega el raycast
                /*GameObject hitParticleEffect = Instantiate(hitParticles, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                hitParticleEffect.transform.SetParent(hit.transform);
                //Esto crea una imagen justo donde pega el raycast
                GameObject bulletHole = Instantiate(bulletImpact, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
                bulletHole.transform.SetParent(hit.transform);

                Destroy(hitParticleEffect, 1f);*/
                //Destroy(bulletHole, 4f);

                if (hit.transform.GetComponent<EnemyHealth>())              //Esto pregunta si el objeto al que le disparamos tiene el controlador de vida
                {
                    hit.transform.GetComponent<EnemyHealth>().TakeDamage(damage, hit.point);//, hit.point);//ApplyDamage(damage); //Accedo a la funcion del controlador y le mando la variable damage
                }
            //}
        }

        anim.CrossFadeInFixedTime("Shot", 0.01f);                           //Ejecuta la animacion de disparo
        muzzleFlash.Play();                                                 //Ejecuta el fogonazo del arma
        PlayShootSound();                                                   //Ejecuta el sonido del disparo

        currentBullets--;
        UpdadateAmmoText();

        fireTimer = 0.0f;                                                   //Resetea el fire timer
    }

    public void Reload()
    {
        if (bulletsLeft <= 0) return;                                       //Si no te quedan mas balas no podes recargar

        int bulletsToLoad = bulletsPerMag - currentBullets;                 //Esto se fija cuantas balas hay que cargar en el cargador
        int bulletsToDeduct = (bulletsLeft >= bulletsToLoad) ? bulletsToLoad : bulletsLeft;

        bulletsLeft -= bulletsToDeduct;
        currentBullets += bulletsToDeduct;

        UpdadateAmmoText();
    }

    private void DoReload()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

        if (isReloading) return;

        anim.CrossFadeInFixedTime("Recharge", 0.01f);
    }

    private void PlayShootSound()
    {
        _AudioSource.PlayOneShot(shootSound);                               //Con este podemos reproducir muchos audios
        //_AudioSource.clip = shootSound;                                   //Con estos dos el audio se cortaba
        //_AudioSource.Play();
    }

    private void UpdadateAmmoText()
    {
        ammoText.text = currentBullets + " / " + bulletsLeft;
    }

    /*IEnumerator LoseScene()
    {
        //thisWeapon.SetActive(false);
        float fadeTime = GameObject.Find("Fade").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("Lose");
    }*/
}