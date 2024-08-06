using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NamesRelated : MonoBehaviour
{
    public static NamesRelated Instance;

    public GameObject saveButton;
    public int maxEntries = 50;
    public int totalLines;
    public TMP_Text totalEntries;
    public List<TMP_InputField> allInputs;
    public List<MultiLineInputHandler> allLinesHandlers;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CallSave()
    {
        foreach(MultiLineInputHandler handler in allLinesHandlers)
        {
            handler.SaveEntries();
        }
        saveButton.SetActive(false);
    }
}
