using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

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
        DataContainer.Instance.randomEntries = new List<string>(NamesRelated.Instance.allLinesHandlers[0].entries);
        DataContainer.Instance.divineEntries = new List<string>(NamesRelated.Instance.allLinesHandlers[1].entries);
        DataContainer.Instance.rootEntries = new List<string>(NamesRelated.Instance.allLinesHandlers[2].entries);
        DataContainer.Instance.paragonEntries = new List<string>(NamesRelated.Instance.allLinesHandlers[3].entries);
        DataContainer.Instance.ordinamEntries = new List<string>(NamesRelated.Instance.allLinesHandlers[4].entries);

        SceneManager.LoadScene(1);
    }
}
