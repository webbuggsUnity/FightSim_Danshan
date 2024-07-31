using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultiLineInputHandler : MonoBehaviour
{
    public TMP_Text totalEntries;
    public TMP_InputField inputField;
    public int maxCharactersPerLine = 15;
    public int maxEntries = 50;
    public List<string> defaultEntries = new List<string> { "James", "Alex", "Thomas", "Aliesha", "Maria" };

    public GameObject saveButton;

    // Made public for debugging purposes
    public List<string> entries = new List<string>();
    int restore = 0;
    private void Start()
    {
        // Add listeners
        inputField.onValueChanged.AddListener(OnValueChanged);
        //inputField.onEndEdit.AddListener(OnSubmit);

        // Ensure input field is set to multi-line mode
        inputField.lineType = TMP_InputField.LineType.MultiLineNewline;

        // Restore previously saved entries
        RestoreEntries();
    }

    private void OnValueChanged(string text)
    {
        string[] lines = text.Split('\n');

        // Check for line count and character length constraints
        if (lines.Length > maxEntries)
        {
            lines = lines[0..maxEntries]; // Trim lines to maxEntries
        }

        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();

            // Remove spaces and truncate line if needed
            if (line.Length > maxCharactersPerLine)
            {
                line = line.Substring(0, maxCharactersPerLine);
            }

            if (line.Contains(" "))
            {
                line = line.Replace(" ", ""); // Remove spaces
            }

            lines[i] = line;
        }

        string processedText = string.Join("\n", lines);
        if (processedText != inputField.text)
        {
            UpdateInputFieldText(processedText);
        }

        if (restore == 1)
            saveButton.SetActive(true);

        restore = 1;
    }

    public void OnSubmit()
    {
        Debug.Log("Submit: ");
        ProcessEntries();
        SaveEntries();
        saveButton.SetActive(false);
    }

    private void ProcessEntries()
    {
        entries.Clear();
        string[] lines = inputField.text.Split('\n');

        // Process each line and enforce constraints
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].Trim();

            if (line.Length > maxCharactersPerLine)
            {
                line = line.Substring(0, maxCharactersPerLine);
            }

            if (!string.IsNullOrEmpty(line) && entries.Count < maxEntries)
            {
                entries.Add(line);
            }
        }

        // Update the input field text to reflect the processed entries
        string newText = string.Join("\n", entries);
        if (newText != inputField.text)
        {
            UpdateInputFieldText(newText);
        }
    }

    private void UpdateInputFieldText(string newText)
    {
        Debug.Log("Updating Input Field Text: " + newText);
        inputField.text = newText;
    }

    private void SaveEntries()
    {
        string entriesText = string.Join("\n", entries);
        PlayerPrefs.SetString("MultiLineInput", entriesText);
        PlayerPrefs.Save();
        Debug.Log("Entries Saved: " + entriesText);
        totalEntries.text = $"Players Count: {entries.Count} / {maxEntries}";
    }

    public void RestoreEntries()
    {
        if (PlayerPrefs.HasKey("MultiLineInput"))
        {
            string savedEntries = PlayerPrefs.GetString("MultiLineInput");
            Debug.Log("Restored Entries: " + savedEntries);
            entries = new List<string>(savedEntries.Split('\n'));
            entries.RemoveAll(entry => string.IsNullOrEmpty(entry));
        }
        else
        {
            entries = new List<string>(defaultEntries);
        }
        UpdateInputFieldText(string.Join("\n", entries));
        SaveEntries();
    }
}
