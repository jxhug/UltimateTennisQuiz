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
    private Question currentQuestion;

    private int randomQuestionIndex;
    private int randomPermutationIndex;
    private int[,] randomAnswerPermutations = new int[6, 4] { { 1, 0, 2, 3 }, { 2, 1, 3, 0 }, { 2, 3, 1, 0 }, { 1, 2, 0, 3 }, { 0, 3, 2, 1 }, { 3, 0, 2, 1 } };
    private int correctAnswerCellIndex;

    [SerializeField]
    private float questionTransitionTime = 2.5f;

    [SerializeField]
    private TMPro.TMP_Text questionText;

    [SerializeField]
    private TMPro.TMP_Text[] answerText = new TMPro.TMP_Text[4];

    [SerializeField]
    private GameObject correctAnswerUI;

    [SerializeField]
    private GameObject wrongAnswerUI;

    void Start()
    {
        var jsonTextFile = Resources.Load<TextAsset>("TennisQuestions");
        JSONQuestions = JsonUtility.FromJson<QuestionList>(jsonTextFile.text);
        UnansweredQuestions = JSONQuestions.Questions;

        SetCurrentQuestion();
    }

    void SetCurrentQuestion()  
    {
        correctAnswerUI.SetActive(false);
        wrongAnswerUI.SetActive(false);

        randomQuestionIndex = Random.Range(0, UnansweredQuestions.Count);
        currentQuestion = UnansweredQuestions[randomQuestionIndex];
        questionText.text = currentQuestion.QuestionText;

        randomPermutationIndex = Random.Range(0, randomAnswerPermutations.GetLength(0));

        for (int cellIndex = 0; cellIndex <= 3; cellIndex++)
        {
            int answerIndex = randomAnswerPermutations[randomPermutationIndex, cellIndex];
            answerText[cellIndex].text = currentQuestion.Answers[answerIndex];

            if (answerIndex == currentQuestion.CorrectAnswerIndex)
            {
                correctAnswerCellIndex = cellIndex;
            }
        }
    }

    IEnumerator TransitionToNextQuestion() 
    {
        UnansweredQuestions.Remove(currentQuestion);

        yield return new WaitForSeconds(questionTransitionTime);

        SetCurrentQuestion();
    }

    public void CheckAnswer(int SelectedIndex)
    {
        if (SelectedIndex == correctAnswerCellIndex)
        {
            correctAnswerUI.SetActive(true);
        }
        else
        {
            wrongAnswerUI.SetActive(true);
        }
        StartCoroutine(TransitionToNextQuestion());
    }
}