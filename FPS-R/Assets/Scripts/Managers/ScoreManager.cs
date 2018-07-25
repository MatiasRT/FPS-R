using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int score;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text winText;

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
            Debug.Log("ganaste");
            if (winText != null)
                winText.enabled = true;
            Time.timeScale = 0.1f;
            Invoke("LoadWin", 0.5f);
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
