using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class StartButtons : MonoBehaviour {

    [SerializeField] private bool quit = false;
    [SerializeField] string nextScene;
    /*
    Color letra = GameObject.Find("Comenzar").GetComponent<TextMesh>().color;

    public void OnMouseEnter()
    {
        
        Debug.Log("Hola");
        GameObject.Find("Comenzar").GetComponent<TextMesh>().color = Color.white;
        GameObject.Find("Salir").GetComponent<TextMesh>().color = Color.white;
    }

    public void OnMouseExit()
    {
        GameObject.Find("Comenzar").GetComponent<TextMesh>().color = letra;
        GameObject.Find("Salir").GetComponent<TextMesh>().color = letra;
    }
    */
    /*public void OnMouseUp()
    {
        if (quit == true)
            GameExit();
        else
            LoadGame();
    }

    public void GameExit()
    {
        Application.Quit();
        Debug.Log("Exit");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Tutorial01");
        
    }*/

    public void Awake()
    {
        var btn = GetComponent<Button>();
        btn.onClick.AddListener(LoadGame);
        btn.onClick.AddListener(GameExit);
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(nextScene);
    }
}
