using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandleIdentifyMenu : MonoBehaviour
{
    private LabItem[] LabItems =
    {
        LabItem.Wafer,
        LabItem.Photoresist,
        LabItem.MaskAligner,
        LabItem.NitrogenGun,
        LabItem.GeneralHood,
        LabItem.CorrosiveHood,
        LabItem.Spincoater,
        LabItem.Oven,
        LabItem.SolventWasteContainer,
        LabItem.AcidWasteContainer,
    };

    private int itemIndex = 0;
    public TextMeshProUGUI TextObject;
    public TextMeshProUGUI StepTextObject;
    public Button NextButton;
    private AudioManagerUtil AudioManager;

    public LabItem GetItem()
    {
        return LabItems[itemIndex];
    }

    public void SetNextItem()
    {
        if (IsLastItem())
        {
            TextObject.text = "Well done!";
            NextButton.interactable = false;
            return;
        }
        itemIndex++;
        AudioManager.PlayClip(AudioManager.BeepClip);
        RenderText();
    }

    public bool IsLastItem()
    {
        return itemIndex == LabItems.Length - 1;
    }

    private string ConvertPascalCaseToSpaces(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input;

        return Regex.Replace(input, "(?<!^)([A-Z])", " $1");
    }

    private void RenderText()
    {
        TextObject.text = ConvertPascalCaseToSpaces(GetItem().ToString());
        StepTextObject.text = $"{itemIndex + 1} / {LabItems.Length}";
        NextButton.interactable = false;
    }



    public void HandleSelect(LabItem labItem)
    {
        if (labItem != GetItem()) return;


        NextButton.interactable = true;
    }

    public void ResetItems()
    {
        itemIndex = 0;
        RenderText();
    }

    private void OnEnable()
    {
        AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManagerUtil>();
        NextButton.onClick.AddListener(SetNextItem);

        ResetItems();
    }

    private void OnDisable()
    {
        NextButton.onClick.RemoveListener(SetNextItem);
    }
}
