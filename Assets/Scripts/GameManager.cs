using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using VariableSchemaNS;

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

    private float questionTransitionTime = 3.5f;

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

    [SerializeField]
    private TMPro.TMP_Text correctAnswerInfoText;

    [SerializeField]
    private TMPro.TMP_Text incorrectAnswerInfoText;

    [SerializeField]
    private Sprite winningImage;

   

    //Multiplayer end screen objects
    private GameObject multiplayerFinalScoreScreen;
    private GameObject[] multiplayerImages;
    private GameObject[] multiplayerWinnerImages;
    private TMPro.TMP_Text[] multiplayerImageTexts;
    private Image multiplayerWinningImage;

    private int numberQuestionsPerPlayer = 3;
    private int numberQuestionsInGame;

    private int numberPlayersInGame;
    private int currentPlayer;

    private int[] scores;
    public static int highScore = 0;

    private static System.Random rng = new System.Random();

    private QuizScreens quizScreens;

    void Start()
    {
        mainQuestionScreen.SetActive(true);
        finalSingleplayerScoreScreen.SetActive(false);
        quizScreens.twoPlayerFinalScoreScreen.SetActive(false);
        quizScreens.threePlayerFinalScoreScreen.SetActive(false);
        quizScreens.fourPlayerFinalScoreScreen.SetActive(false);

        var jsonTextFile = Resources.Load<TextAsset>("BigThreeQuestions");
        jsonQuestions = JsonUtility.FromJson<QuestionList>(jsonTextFile.text);
        allQuestions = jsonQuestions.questions.OrderBy(a => rng.Next()).ToList();

        numberPlayersInGame = PlayerSelect.numberPlayersInGame;
        numberQuestionsInGame = numberPlayersInGame * numberQuestionsPerPlayer;

        switch (numberPlayersInGame)
        {
            case 2:
                multiplayerFinalScoreScreen = quizScreens.twoPlayerFinalScoreScreen;
                multiplayerImages = quizScreens.twoPlayerImages;
                multiplayerWinnerImages = quizScreens.twoPlayerWinnerImages;
                multiplayerImageTexts = quizScreens.twoPlayerImageTexts;
                multiplayerWinningImage = quizScreens.twoPlayerWinningImage;
                break;
            case 3:
                multiplayerFinalScoreScreen = quizScreens.threePlayerFinalScoreScreen;
                multiplayerImages = quizScreens.threePlayerImages;
                multiplayerWinnerImages = quizScreens.threePlayerWinnerImages;
                multiplayerImageTexts = quizScreens.threePlayerImageTexts;
                multiplayerWinningImage = quizScreens.threePlayerWinningImage;
                break;
            case 4:
                multiplayerFinalScoreScreen = quizScreens.fourPlayerFinalScoreScreen;
                multiplayerImages = quizScreens.fourPlayerImages;
                multiplayerWinnerImages = quizScreens.fourPlayerWinnerImages;
                multiplayerImageTexts = quizScreens.fourPlayerImageTexts;
                multiplayerWinningImage = quizScreens.fourPlayerWinningImage;
                break;
        }

        scores = new int[numberPlayersInGame];
        for (int i = 0; i < numberPlayersInGame; i++)
        {
            scores[i] = 0;
            if (numberPlayersInGame > 1)
            {
                multiplayerImages[i].SetActive(false);
            }
        }

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
                SingleplayerEndScreen();
            else
                MultiplayerEndScreen();
        }
        else
        {
            currentPlayer = (currentPlayer + 1) % numberPlayersInGame;
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
            correctAnswerInfoText.text = currentQuestion.info;
            scores[currentPlayer]++;
        }
        else
        {
            wrongAnswerUI.SetActive(true);
            incorrectAnswerInfoText.text = currentQuestion.info;
        }

        StartCoroutine(TransitionToNextQuestion());
    }

    void SingleplayerEndScreen()
    {
        if (scores[0] > highScore)
        {
            highScore = scores[0];
        }
        finalSingleplayerScoreScreen.SetActive(true);
        finalSingleplayerScoreText.text = ("Your final score is: " + scores[0] + "/" + numberQuestionsPerPlayer);
        highScoreText.text = ("Your highest score is: " + highScore + "/" + numberQuestionsPerPlayer);
    }

    void MultiplayerEndScreen()
    {
        multiplayerFinalScoreScreen.SetActive(true);
        int winningScore = scores.Max();
        for (int i = 0; i < numberPlayersInGame; i++)
        {
            multiplayerImages[i].SetActive(true);
            multiplayerWinnerImages[i].SetActive(false);
            multiplayerImageTexts[i].text = ("Player " + (i + 1) + "'s Final Score:" + scores[i] + "/" + numberQuestionsPerPlayer);

            if (scores[i] == winningScore && scores[i] != 0)
            {
                // You know that the i’th player is the winning player (there may be more than one winning player)
                multiplayerWinningImage = multiplayerImages[i].GetComponent<Image>();
                multiplayerWinningImage.sprite = winningImage;
                multiplayerWinnerImages[i].SetActive(true);
            }
        }
    }
}   