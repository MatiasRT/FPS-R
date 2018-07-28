using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int score;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text winText;
    [SerializeField] AudioSource win;
    [SerializeField] AudioSource bkm;

    void Awake()
    {
        if (winText != null)
            winText.enabled = false;
        score = 0;
    }


    void Update()
    {

        if (score >= 666f)
        {
            if (winText != null)
                winText.enabled = true;
            Time.timeScale = 0.1f;
            win.enabled = true;
            bkm.enabled = false;
            Invoke("LoadWin", 1.15f);
        }
        else
            scoreText.text = "Score: " + score;
    }

    void LoadWin()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
