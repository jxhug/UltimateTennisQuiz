using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public QuestionList jsonQuestions;
    public List<Question> allQuestions;
    public List<Question> unansweredQuestions;
    private Question currentQuestion;

    private int randomQuestionIndex;
    private int randomPermutationIndex;
    private int[,] randomAnswerPermutations = new int[6, 4] { { 1, 0, 2, 3 }, { 2, 1, 3, 0 }, { 2, 3, 1, 0 }, { 1, 2, 0, 3 }, { 0, 3, 2, 1 }, { 3, 0, 2, 1 } };
    private int correctAnswerCellIndex;

    [SerializeField]
    private float questionTransitionTime;

    [SerializeField]
    private TMPro.TMP_Text questionText;

    [SerializeField]
    private TMPro.TMP_Text[] answerText = new TMPro.TMP_Text[4];

    [SerializeField]
    private GameObject correctAnswerUI;

    [SerializeField]
    private GameObject wrongAnswerUI;

    [SerializeField]
    private int numQuestionsPerGame = 10;

    public int score = 0;
    public static int highScore = 0;


    void Start()
    {
        var jsonTextFile = Resources.Load<TextAsset>("TennisQuestions");
        jsonQuestions = JsonUtility.FromJson<QuestionList>(jsonTextFile.text);
        allQuestions = jsonQuestions.questions;

        // TODO: Randomly select "numQuestionsPerGame" questions from allQuestions list
        unansweredQuestions = allQuestions.Take(numQuestionsPerGame).ToList();

        SetCurrentQuestion();
    }

    void SetCurrentQuestion()  
    {
        randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];
        questionText.text = currentQuestion.questionText;

        randomPermutationIndex = Random.Range(0, randomAnswerPermutations.GetLength(0));

        for (int cellIndex = 0; cellIndex <= 3; cellIndex++)
        {
            int answerIndex = randomAnswerPermutations[randomPermutationIndex, cellIndex];
            answerText[cellIndex].text = currentQuestion.answers[answerIndex];

            if (answerIndex == currentQuestion.correctAnswerIndex)
            {
                correctAnswerCellIndex = cellIndex;
            }
        }
    }

    IEnumerator TransitionToNextQuestion() 
    {
        unansweredQuestions.Remove(currentQuestion);
        if (unansweredQuestions.Count == 0)
        {
            //SceneManager.LoadScene(Scene)
            Debug.Log("No more questions left");
        }
        else
        {
            yield return new WaitForSeconds(questionTransitionTime / 2);
            SetCurrentQuestion();
            yield return new WaitForSeconds(questionTransitionTime / 2);
            correctAnswerUI.SetActive(false);
            wrongAnswerUI.SetActive(false);
        }
    }

    public void CheckAnswer(int SelectedIndex)
    {
        if (SelectedIndex == correctAnswerCellIndex)
        {
            correctAnswerUI.SetActive(true);
            score++;
        }
        else
        {
            wrongAnswerUI.SetActive(true);
        }
        StartCoroutine(TransitionToNextQuestion());
    }
}