using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPause = false;

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerHB;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject playerWeapon;
    //[SerializeField] private AudioSource sound;

    void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause)
                Resume();
            else
                Pause();
        }
	}

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;

        player.GetComponent<PlayerMove>().enabled = true;
        playerHB.GetComponent<HeadBob>().enabled = true;
        playerCamera.GetComponent<PlayerLook>().enabled = true;
        playerWeapon.GetComponent<Weapon>().enabled = true;

        //sound.GetComponent<AudioSource>().enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;

        player.GetComponent<PlayerMove>().enabled = false;
        playerHB.GetComponent<HeadBob>().enabled = false;
        playerCamera.GetComponent<PlayerLook>().enabled = false;
        playerWeapon.GetComponent<Weapon>().enabled = false;

        //sound.GetComponent<AudioSource>().enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadMenu()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
        StartCoroutine(ChangeLevel());
    }

    IEnumerator ChangeLevel()
    {
        float fadeTime = GameObject.Find("Fade").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("Menu");
    }
}
