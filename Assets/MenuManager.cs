using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public MultiLineInputHandler multiLine;
    public GameObject mainMenu,nameSelection;
    public VideoPlayer videoPlayer;
    //public static int

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        //videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Movie_003.mp4");
        //videoPlayer.Play();
    }

    public void BackToMain()
    {
        nameSelection.SetActive(true);
    }

    public void StartGame()
    {
        DataContainer.Instance.entriesData = new List<string>(multiLine.entries);
        SceneManager.LoadScene(1);
    }
}
