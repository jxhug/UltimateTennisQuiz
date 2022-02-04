using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public QuestionList JSONQuestions;
    public List<Question> UnansweredQuestions;
    private Question CurrentQuestion;

    private int RandomQuestionIndex;
    private int RandomPermutationIndex;
    private int[,] RandomAnswerPermutations = new int[6, 4] { { 1, 0, 2, 3 }, { 2, 1, 3, 0 }, { 2, 3, 1, 0 }, { 1, 2, 0, 3 }, { 0, 3, 2, 1 }, { 3, 0, 2, 1 } };
    private int CorrectAnswerCellIndex;

    [SerializeField]
    private float QuestionTransitionTime = 1f;

    [SerializeField]
    private Text QuestionText;

    [SerializeField]
    private Text[] AnswerText = new Text[4];

    [SerializeField]
    private GameObject CorrectAnswerUI;

    [SerializeField]
    private GameObject WrongAnswerUI;

    void Start()
    {
        var jsonTextFile = Resources.Load<TextAsset>("TennisQuestions");
        JSONQuestions = JsonUtility.FromJson<QuestionList>(jsonTextFile.text);
        UnansweredQuestions = JSONQuestions.Questions;

        SetCurrentQuestion();
    }

    void SetCurrentQuestion()  
    {
        CorrectAnswerUI.SetActive(false);
        WrongAnswerUI.SetActive(false);

        RandomQuestionIndex = Random.Range(0, UnansweredQuestions.Count);
        CurrentQuestion = UnansweredQuestions[RandomQuestionIndex];
        QuestionText.text = CurrentQuestion.QuestionText;

        RandomPermutationIndex = Random.Range(0, RandomAnswerPermutations.GetLength(0));

        for (int cellIndex = 0; cellIndex <= 3; cellIndex++)
        {
            int answerIndex = RandomAnswerPermutations[RandomPermutationIndex, cellIndex];
            AnswerText[cellIndex].text = CurrentQuestion.Answers[answerIndex];

            if (answerIndex == CurrentQuestion.CorrectAnswerIndex)
            {
                CorrectAnswerCellIndex = cellIndex;
            }
        }
    }

    IEnumerator TransitionToNextQuestion() 
    {
        UnansweredQuestions.Remove(CurrentQuestion);

        yield return new WaitForSeconds(QuestionTransitionTime);

        SetCurrentQuestion();
    }

    public void CheckAnswer(int SelectedIndex)
    {
        if (SelectedIndex == CorrectAnswerCellIndex)
        {
            CorrectAnswerUI.SetActive(true);
        }
        else
        {
            WrongAnswerUI.SetActive(true);
        }
        StartCoroutine(TransitionToNextQuestion());
    }
}