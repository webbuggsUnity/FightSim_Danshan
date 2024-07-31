using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public GameObject mainMenu,nameSelection;
    //public static int

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    public void BackToMain()
    {
        nameSelection.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
