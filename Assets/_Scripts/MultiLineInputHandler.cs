using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultiLineInputHandler : MonoBehaviour
{
    public TMP_InputField inputField;
    public int maxCharactersPerLine = 15;
    public int maxEntries = 50;
    public TMP_Text totalNamesEnetered;

    public List<string> entries = new List<string>();

    private void Start()
    {
        inputField.onValueChanged.AddListener(OnValueChanged);
        inputField.onEndEdit.AddListener(OnSubmit);
        inputField.lineType = TMP_InputField.LineType.MultiLineNewline;
        RestoreEntries();
    }

    private void OnValueChanged(string text)
    {
        // Enforce the max number of lines
        string[] lines = text.Split('\n');
        if (lines.Length > maxEntries)
        {
            text = string.Join("\n", lines, 0, maxEntries);
            inputField.text = text;
            inputField.caretPosition = inputField.text.Length;
            return;
        }
        //ProcessEntries();
        //SaveEntries();
        // Validate lines but don't update entries here
    }

    private void OnSubmit(string text)
    {
        // Process and save entries when editing ends
        ProcessEntries();
        SaveEntries();
    }

    private void ProcessEntries()
    {
        entries.Clear();
        string[] lines = inputField.text.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Replace(" ", ""); // Remove spaces

            if (line.Length > maxCharactersPerLine)
            {
                line = line.Substring(0, maxCharactersPerLine);
            }

            if (!string.IsNullOrEmpty(line))
            {
                if (entries.Count < maxEntries) // Enforce max entries
                {
                    entries.Add(line);
                }
            }
        }

        // Rebuild the inputField.text from entries
        string newText = string.Join("\n", entries);
        if (newText != inputField.text)
        {
            inputField.text = newText;
            inputField.caretPosition = inputField.text.Length;
        }
    }

    public void RemoveEntry(string entry)
    {
        if (entries.Contains(entry))
        {
            entries.Remove(entry);
            // Reconstruct the text excluding the removed entry
            string newText = string.Join("\n", entries);
            inputField.text = newText;
            inputField.ActivateInputField();
            inputField.caretPosition = inputField.text.Length;
            SaveEntries();
        }
    }

    private void SaveEntries()
    {
        PlayerPrefs.SetString("MultiLineInput", inputField.text);
        PlayerPrefs.Save();
    }

    private void RestoreEntries()
    {
        if (PlayerPrefs.HasKey("MultiLineInput"))
        {
            string savedEntries = PlayerPrefs.GetString("MultiLineInput");
            inputField.text = savedEntries;
            entries = new List<string>(savedEntries.Split('\n'));

            // Remove any empty entries
            entries.RemoveAll(entry => string.IsNullOrEmpty(entry));
        }
    }

    private void OnEnable()
    {
        RestoreEntries();
    }

    private void OnDisable()
    {
        SaveEntries();
    }
}
