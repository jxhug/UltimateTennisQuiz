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
    private float questionTransitionTime = 2.5f;

    [SerializeField]
    private TMPro.TMP_Text questionText;

    [SerializeField]
    private TMPro.TMP_Text[] answerText = new TMPro.TMP_Text[4];

    [SerializeField]
    private GameObject correctAnswerUI;

    [SerializeField]
    private GameObject wrongAnswerUI;

    [SerializeField]
    private GameObject mainQuestionScreen;

    [SerializeField]
    private GameObject finalSingleplayerScoreScreen;

    [SerializeField]
    private TMPro.TMP_Text finalSingleplayerScoreText;

    [SerializeField]
    private TMPro.TMP_Text highScoreText;

    [SerializeField]
    private TMPro.TMP_Text numberPlayerText;

    //Four player end screen
    [SerializeField]
    private GameObject fourPlayerFinalScoreScreen;
    [SerializeField]
    private GameObject[] fourPlayerImages = new GameObject[4];
    private int largestFourPlayerScore;

    private int numberQuestionsPerPlayer = 5;
    private int numberQuestionsInGame;

    private int numberPlayersInGame;
    private int currentPlayer;

    private int[] scores;
    public static int highScore = 0;


    void Start()
    {
        mainQuestionScreen.SetActive(true);
        finalSingleplayerScoreScreen.SetActive(false);
        fourPlayerFinalScoreScreen.SetActive(false);

        var jsonTextFile = Resources.Load<TextAsset>("TennisQuestions");
        jsonQuestions = JsonUtility.FromJson<QuestionList>(jsonTextFile.text);
        allQuestions = jsonQuestions.questions;

        numberPlayersInGame = PlayerSelect.numberPlayersInGame;
        numberQuestionsInGame = numberPlayersInGame * numberQuestionsPerPlayer;

        scores = new int[numberPlayersInGame];
        for (int i = 0; i < numberPlayersInGame; i++)
        {
            scores[i] = 0;
            fourPlayerImages[i].SetActive(false);
        }

        // TODO: Randomly select "numberQuestionsInGame" questions from allQuestions list
        unansweredQuestions = allQuestions.Take(numberQuestionsInGame).ToList();

        currentPlayer = 0;
        SetCurrentQuestion();
    }

    void SetCurrentQuestion()
    {
        numberPlayerText.text = ("Player " + (currentPlayer + 1));
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

        // Fade to correct / incorrect display
        yield return new WaitForSeconds(questionTransitionTime / 2);

        // Set up the next question or switch to the finish scene
        if (unansweredQuestions.Count == 0)
        {
            mainQuestionScreen.SetActive(false);

            if (numberPlayersInGame == 1)
            {
                if (scores[0] > highScore)
                {
                    highScore = scores[0];
                }
                finalSingleplayerScoreScreen.SetActive(true);
                finalSingleplayerScoreText.text = ("Your final score is: " + scores[0] + "/" + numberQuestionsPerPlayer);
                highScoreText.text = ("Your highest score is: " + highScore + "/" + numberQuestionsPerPlayer);
            }
            else
            {
                if (numberPlayersInGame == 4)
                {
                    FourPlayerEndScreen();
                }
            }
        }
        else
        {
            Debug.Log("Before increment current Player is :" + currentPlayer);
            currentPlayer = (currentPlayer + 1) % numberPlayersInGame;
            Debug.Log("After increment current Player is :" + currentPlayer);

            SetCurrentQuestion();
        }
        // Fade into the next question
        yield return new WaitForSeconds(questionTransitionTime / 2);
        correctAnswerUI.SetActive(false);
        wrongAnswerUI.SetActive(false);
    }

    public void CheckAnswer(int SelectedIndex)
    {
        if (SelectedIndex == correctAnswerCellIndex)
        {
            correctAnswerUI.SetActive(true);
            scores[currentPlayer]++;
        }
        else
        {
            wrongAnswerUI.SetActive(true);
        }

        StartCoroutine(TransitionToNextQuestion());
    }

    void FourPlayerEndScreen()
    {
        fourPlayerFinalScoreScreen.SetActive(true);
        for (int i = 0; i < numberPlayersInGame; i++)
        {
            fourPlayerImages[i].SetActive(true);
        }
        largestFourPlayerScore = (scores[0] > scores[1] && scores[0] > scores[1] && scores[0] > scores[1]) ? scores[0] : (scores[1] > scores[2] && scores[1] > scores[3]) ? scores[1] : (scores[2] > scores[3]) ? scores[2] : scores[3];

    }
}   