using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceDisplay : MonoBehaviour
{
    [SerializeField] string resourceName = string.Empty;
    [SerializeField] public List<int> resourceAmounts = new List<int>();

    TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        string displayText = $"{resourceName}:  {resourceAmounts[0]}";


        for (int i = 1; i < resourceAmounts.Count; i++)
        {
            displayText = $"{displayText} / {resourceAmounts[i]}";
        }

        text.SetText(displayText);
    }

}
