using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandleQuizMenu : MonoBehaviour
{
    private class QuizItem
    {
        public string Question { get; set; }
        public char Answer { get; set; }
        public (char, string)[] Options { get; set; }

        public QuizItem(string question, char answer, (char, string)[] options)
        {
            Question = question;
            Answer = answer;
            Options = options;
        }
    }

    private List<QuizItem> quizItems = new()
    {
        new QuizItem(
            "What is the primary purpose of photolithography in semiconductor manufacturing?",
            'C',
            new (char, string)[] {
                ('A', "To clean the wafer surface"),
                ('B', "To deposit layers of material"),
                ('C', "To transfer patterns onto a substrate")
            }
        ),
        new QuizItem(
            "Which type of light is typically used in the photolithography process?",
            'B',
            new (char, string)[] {
                ('A', "Infrared light"),
                ('B', "Ultraviolet light"),
                ('C', "Visible light")
            }
        ),
        new QuizItem(
            "What material is commonly used as a photoresist in photolithography?",
            'B',
            new (char, string)[] {
                ('A', "Silicon"),
                ('B', "Polymers"),
                ('C', "Metal alloys")
            }
        ),
        new QuizItem(
            "During the photolithography process, what is the role of a photomask?",
            'B',
            new (char, string)[] {
                ('A', "To protect the wafer from light exposure"),
                ('B', "To block certain areas of light and create patterns"),
                ('C', "To increase the resolution of the pattern")
            }
        ),
        new QuizItem(
            "What does the term 'resolution' refer to in photolithography?",
            'C',
            new (char, string)[] {
                ('A', "The wavelength of light used"),
                ('B', "The thickness of the photoresist layer"),
                ('C', "The smallest feature size that can be accurately printed")
            }
        ),
        new QuizItem(
            "Which of the following is a commonly used technique to improve resolution in photolithography?",
            'B',
            new (char, string)[] {
                ('A', "Using visible light"),
                ('B', "Reducing the wavelength of the light source"),
                ('C', "Increasing the distance between the photomask and wafer")
            }
        ),
        new QuizItem(
            "What is the typical order of steps in a photolithography process?",
            'B',
            new (char, string)[] {
                ('A', "Exposure, development, spin coating, baking"),
                ('B', "Spin coating, exposure, baking, development"),
                ('C', "Baking, spin coating, development, exposure")
            }
        ),
        new QuizItem(
            "Which type of photolithography uses electrons instead of light to achieve higher resolutions?",
            'B',
            new (char, string)[] {
                ('A', "Optical lithography"),
                ('B', "Electron beam lithography"),
                ('C', "X-ray lithography")
            }
        ),
        new QuizItem(
            "What is 'photoresist' sensitivity dependent on?",
            'A',
            new (char, string)[] {
                ('A', "The exposure time to light"),
                ('B', "The thickness of the wafer"),
                ('C', "The temperature of the substrate")
            }
        ),
        new QuizItem(
            "What is the purpose of the 'baking' step in photolithography?",
            'B',
            new (char, string)[] {
                ('A', "To harden the wafer surface"),
                ('B', "To improve adhesion and remove solvents from the photoresist"),
                ('C', "To etch the substrate")
            }
        )
    };

    private int itemIndex = 0;

    public TextMeshProUGUI QuestionText;

    public Button NextButton;
    public GameObject OptionGroup;
    private int Score;

    private AudioManagerUtil AudioManager;


    public TextMeshProUGUI GetTextComponent(GameObject button)
    {
        return button.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }


    public void GetNextQuestion()
    {
        if (IsLastItem())
        {
            QuestionText.text = $"Score: {Score} %";
            OptionGroup.SetActive(false);
            NextButton.interactable = false;
            AudioManager.PlayClip(AudioManager.WellDoneClip);
            return;
        }
        itemIndex++;
        RenderQuestion();
    }


    private bool IsLastItem()
    {
        return itemIndex == quizItems.Count - 1;
    }

    public void RenderQuestion()
    {
        var currentQuestion = quizItems[itemIndex];

        QuestionText.text = currentQuestion.Question;

        for (int i = 0; i < OptionGroup.transform.childCount; i++)
        {
            var optionObject = OptionGroup.transform.GetChild(i).gameObject;
            var textObject = GetTextComponent(optionObject);
            textObject.text = $"    {currentQuestion.Options[i].Item1}. {currentQuestion.Options[i].Item2}";
        }


        NextButton.interactable = false;
    }

    public void ResetItems()
    {
        itemIndex = 0;
        Score = 100;
        OptionGroup.SetActive(true);
        RenderQuestion();
    }

    public void AnswerQuestion(string answer)
    {
        var currentQuestion = quizItems[itemIndex];

        var answerChar = answer[0];

        Dictionary<char, int> answerDict = new()
        {
            { 'A', 0 },
            { 'B', 1 },
            { 'C', 2 }
        };

        var index = answerDict[answerChar];



        if (answerChar == currentQuestion.Answer)
        {
            var optionObject = OptionGroup.transform.GetChild(index).gameObject;

            NextButton.interactable = true;
            AudioManager.PlayClip(AudioManager.SuccessClip);
        }
        else
        {
            int penalty = 5;
            if (Score > 0) Score -= penalty;
            AudioManager.PlayClip(AudioManager.ErrorClip);
        }
    }

    private void OnEnable()
    {
        AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManagerUtil>();
        ResetItems();

        NextButton.onClick.AddListener(GetNextQuestion);

        for (int i = 0; i < OptionGroup.transform.childCount; i++)
        {
            var optionObject = OptionGroup.transform.GetChild(i).gameObject;
            var button = optionObject.GetComponent<Button>();
            var answer = quizItems[itemIndex].Options[i].Item1.ToString();
            button.onClick.AddListener(() => AnswerQuestion(answer));
        }
    }


    private void OnDisable()
    {
        for (int i = 0; i < OptionGroup.transform.childCount; i++)
        {
            var optionObject = OptionGroup.transform.GetChild(i).gameObject;
            var button = optionObject.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
        }

        NextButton.onClick.RemoveAllListeners();
    }


}
