using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HandleLearningMenu : MonoBehaviour
{
    private class Stage
    {
        public string Title { get; set; }
        public List<string> Steps { get; set; }

        public Stage(string title, List<string> steps)
        {
            Title = title;
            Steps = steps;
        }
    }

    private List<Stage> Stages = new()
    {
        new Stage("Clean Wafer", new List<string>
        {
            "Pick up wafer",
            "Place wafer in acetone petri dish",
            "Place wafer in IPA petri dish",
            "Place wafer in DI Water petri dish",
            "Dry wafer with nitrogen gun"
        }),
        new Stage("Application of photoresist", new List<string>
        {
            "Prebake wafer",
            "Apply photoresist to wafer",
            "Spread resist on wafer with spincoater",
            "Postbake the wafer"
        }),
        new Stage("Exposure", new List<string>
        {
            "Place wafer on substrate holder",
            "Expose wafer to UV light",
            "Post exposure bake"
        }),
        new Stage("Development", new List<string>
        {
            "Develop wafer in developer",
            "Rinse wafer in DI Water",
            "Dry wafer with nitrogen gun"
        })
    };

    public int StageIndex { get; set; } = 0;
    public int StepIndex { get; set; } = 0;

    public Button NextStageButton;
    public GameObject StepsGroup;
    public TextMeshProUGUI StageTitleText;
    private List<GameObject> StepObjects = new();

    private AudioManagerUtil AudioManager;



    public bool IsLastStage()
    {
        return StageIndex == Stages.Count - 1;
    }

    public bool IsLastStep()
    {
        return StepIndex == Stages[StageIndex].Steps.Count - 1;
    }

    public void GetNextStage()
    {
        if (IsLastStage())
        {
            AudioManager.PlayClip(AudioManager.WellDoneClip);
            return;
        }

        StageIndex++;
        StepIndex = 0;
        RenderStage();
        RenderActiveStep();
    }

    //public void GetNextStep()
    //{
    //    if (IsLastStep())
    //    {
    //        NextStageButton.interactable = true;
    //        return;
    //    };

    //    StepIndex++;
    //    RenderActiveStep();
    //}



    private GameObject CreateStepObject(string stepTitle, int index = 0)
    {
        GameObject textObject = new GameObject($"StepObject {index}");

        TextMeshProUGUI textMeshPro = textObject.AddComponent<TextMeshProUGUI>();

        textMeshPro.text = $"{index + 1} - {stepTitle}";
        textMeshPro.fontSize = .035f;
        textMeshPro.alignment = TextAlignmentOptions.Left;

        textMeshPro.color = Color.black;

        textObject.transform.SetParent(gameObject.transform, false);

        RectTransform rectTransform = textObject.GetComponent<RectTransform>();

        rectTransform.sizeDelta = new Vector2(1, .3f);
        rectTransform.anchoredPosition = new Vector2(0, -0.15f - (index * .06f));

        return textObject;


    }

    private void RenderActiveStep()
    {
        for (int i = 0; i < StepObjects.Count; i++)
        {
            var textComponent = StepObjects[i].GetComponent<TextMeshProUGUI>();

            if (i == StepIndex) textComponent.color = Color.white;
            else textComponent.color = Color.black;
        }
    }


    public void RenderStage()
    {
        StageTitleText.text = Stages[StageIndex].Title;

        // Clear all steps
        foreach (var stepObject in StepObjects)
            Destroy(stepObject);

        StepObjects.Clear();

        // Render steps
        for (int i = 0; i < Stages[StageIndex].Steps.Count; i++)
        {
            var step = Stages[StageIndex].Steps[i];
            var textObject = CreateStepObject(step, i);
            StepObjects.Add(textObject);

        }

        // Render Next Buttons
        NextStageButton.interactable = false;
    }

    public void ResetItems()
    {
        StageIndex = 0;
        StepIndex = 0;
        RenderStage();
        RenderActiveStep();
    }


    public void CompleteStep()
    {
        if (IsLastStep())
        {
            NextStageButton.interactable = true;
            AudioManager.PlayClip(AudioManager.SuccessClip);

            if (IsLastStage())
            {
                AudioManager.PlayClip(AudioManager.WellDoneClip);
                NextStageButton.interactable = false;
            }
            return;
        };

        StepIndex++;
        RenderActiveStep();
    }

    private void OnEnable()
    {
        AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManagerUtil>();
        NextStageButton.onClick.AddListener(GetNextStage);
        ResetItems();
    }

    private void OnDisable()
    {
        NextStageButton.onClick.RemoveListener(GetNextStage);
    }

}
