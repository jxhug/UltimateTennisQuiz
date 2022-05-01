using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UtilsNS;
using MenuNS;

public class GameManager : MonoBehaviour
{
    //question objects
    public QuestionList jsonQuestions;

    public List<Question> allQuestions;

    public List<Question> unansweredQuestions;

    private Question currentQuestion;

    private int numberQuestionsPerPlayer;

    private int numberQuestionsInGame;


    //question display objects
    private int randomQuestionIndex;

    private int randomPermutationIndex;

    private int[,] randomAnswerPermutations = new int[6, 4] { { 1, 0, 2, 3 }, { 2, 1, 3, 0 }, { 2, 3, 1, 0 }, { 1, 2, 0, 3 }, { 0, 3, 2, 1 }, { 3, 0, 2, 1 } };

    private int correctAnswerCellIndex;

    private System.Random rng = new System.Random();

    private float questionTransitionTime = 3.5f;


    // portrait two player end screen
    public GameObject portraitTwoPlayerFinalScoreScreen;

    public GameObject[] portraitTwoPlayerImages = new GameObject[2];

    public GameObject[] portraitTwoPlayerWinnerImages = new GameObject[2];

    public TMP_Text[] portraitTwoPlayerImageTexts = new TMP_Text[2];


    //portrait three player end screen
    public GameObject portraitThreePlayerFinalScoreScreen;

    public GameObject[] portraitThreePlayerImages = new GameObject[3];

    public GameObject[] portraitThreePlayerWinnerImages = new GameObject[3];

    public TMP_Text[] portraitThreePlayerImageTexts = new TMP_Text[3];


    //portrait four player end screen
    public GameObject portraitFourPlayerFinalScoreScreen;

    public GameObject[] portraitFourPlayerImages = new GameObject[4];

    public GameObject[] portraitFourPlayerWinnerImages = new GameObject[4];

    public TMP_Text[] portraitFourPlayerImageTexts = new TMP_Text[4];


    // landscape two player end screen
    public GameObject landscapeTwoPlayerFinalScoreScreen;

    public GameObject[] landscapeTwoPlayerImages = new GameObject[2];

    public GameObject[] landscapeTwoPlayerWinnerImages = new GameObject[2];

    public TMP_Text[] landscapeTwoPlayerImageTexts = new TMP_Text[2];


    //landscape three player end screen
    public GameObject landscapeThreePlayerFinalScoreScreen;

    public GameObject[] landscapeThreePlayerImages = new GameObject[3];

    public GameObject[] landscapeThreePlayerWinnerImages = new GameObject[3];

    public TMP_Text[] landscapeThreePlayerImageTexts = new TMP_Text[3];


    //landscape four player end screen
    public GameObject landscapeFourPlayerFinalScoreScreen;

    public GameObject[] landscapeFourPlayerImages = new GameObject[4];

    public GameObject[] landscapeFourPlayerWinnerImages = new GameObject[4];

    public TMP_Text[] landscapeFourPlayerImageTexts = new TMP_Text[4];


    //portrait main question screen objects
    [SerializeField]
    private GameObject portraitMainQuestionScreen;

    [SerializeField]
    private TMP_Text portraitQuestionText;

    [SerializeField]
    private TMP_Text[] portraitAnswerText = new TMP_Text[4];

    [SerializeField]
    private TMP_Text portraitNumberPlayerText;


    //landscape main question screen objects
    [SerializeField]
    private GameObject landscapeMainQuestionScreen;

    [SerializeField]
    private TMP_Text landscapeQuestionText;

    [SerializeField]
    private TMP_Text[] landscapeAnswerText = new TMP_Text[4];

    [SerializeField]
    private TMP_Text landscapeNumberPlayerText;


    //portrait singleplayer end screen objects
    [SerializeField]
    private GameObject portraitSingleplayerFinalScoreScreen;

    [SerializeField]
    private TMP_Text portraitSingleplayerScoreText;

    [SerializeField]
    private TMP_Text portraitSingleplayerHighScoreText;


    //landscape singleplayer end screen objects
    [SerializeField]
    private GameObject landscapeSingleplayerFinalScoreScreen;

    [SerializeField]
    private TMP_Text landscapeSingleplayerScoreText;

    [SerializeField]
    private TMP_Text landscapeSingleplayerHighScoreText;


    //portrait multiplayer end screen objects
    private GameObject portraitMultiplayerFinalScoreScreen;

    private GameObject[] portraitMultiplayerImages;

    private GameObject[] portraitMultiplayerWinnerImages;

    private TMP_Text[] portraitMultiplayerImageTexts;

    private Image portraitMultiplayerWinningImage;


    //landscape multiplayer end screen objects
    private GameObject landscapeMultiplayerFinalScoreScreen;

    private GameObject[] landscapeMultiplayerImages;

    private GameObject[] landscapeMultiplayerWinnerImages;

    private TMP_Text[] landscapeMultiplayerImageTexts;

    private Image landscapeMultiplayerWinningImage;


    //canvas objects
    [SerializeField]
    private GameObject portraitCanvas;

    [SerializeField]
    private GameObject landscapeCanvas;


    //player objects
    private int numberPlayersInGame;

    private int currentPlayer;


    //score objects
    private int[] scores;

    private int highScore;


    //answer UI objects
    [SerializeField]
    private GameObject correctAnswerUI;

    [SerializeField]
    private GameObject incorrectAnswerUI;

    [SerializeField]
    private TMP_Text correctAnswerInfoText;

    [SerializeField]
    private TMP_Text incorrectAnswerInfoText;


    //orange gradient object
    [SerializeField]
    private Sprite winningImage;


    private Utils utils;


    //methods
    void Start()
    {
        utils = new Utils();

        numberQuestionsPerPlayer = Menu.GetNumberOfQuestionsPerGame();
        highScore = PlayerPrefs.GetInt("highScore", 0);

        portraitMainQuestionScreen.SetActive(true);
        landscapeMainQuestionScreen.SetActive(true);

        portraitSingleplayerFinalScoreScreen.SetActive(false);
        landscapeSingleplayerFinalScoreScreen.SetActive(false);

        portraitTwoPlayerFinalScoreScreen.SetActive(false);
        portraitThreePlayerFinalScoreScreen.SetActive(false);
        portraitFourPlayerFinalScoreScreen.SetActive(false);
        landscapeTwoPlayerFinalScoreScreen.SetActive(false);
        landscapeThreePlayerFinalScoreScreen.SetActive(false);
        landscapeFourPlayerFinalScoreScreen.SetActive(false);

        correctAnswerUI.SetActive(false);
        incorrectAnswerUI.SetActive(false);

        utils.CheckIfOrientationUpdated(portraitCanvas, landscapeCanvas, true);

        var jsonTextFile = Resources.Load<TextAsset>("BigThreeQuestions");
        jsonQuestions = JsonUtility.FromJson<QuestionList>(jsonTextFile.text);
        allQuestions = jsonQuestions.questions.OrderBy(a => rng.Next()).ToList();

        numberPlayersInGame = PlayerSelect.numberPlayersInGame;
        numberQuestionsInGame = numberPlayersInGame * numberQuestionsPerPlayer;

        if (numberPlayersInGame == 1)
        {
            //set inactive
            portraitNumberPlayerText.gameObject.SetActive(false);
            landscapeNumberPlayerText.gameObject.SetActive(false);

            // change rect transform
            portraitQuestionText.gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(50, 50);
            portraitQuestionText.gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(-50, -50);
            landscapeQuestionText.gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(50, 50);
            landscapeQuestionText.gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(-50, -50);
        }
        else
        {
            //set active
            portraitNumberPlayerText.gameObject.SetActive(true);
            landscapeNumberPlayerText.gameObject.SetActive(true);

            // change rect transform
            portraitQuestionText.gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(50, 50);
            portraitQuestionText.gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(-50, -215);
            landscapeQuestionText.gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(50, 50);
            landscapeQuestionText.gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(-50, -135);
        }

        switch (numberPlayersInGame)
        {
            case 2:
                portraitMultiplayerFinalScoreScreen = portraitTwoPlayerFinalScoreScreen;
                landscapeMultiplayerFinalScoreScreen = landscapeTwoPlayerFinalScoreScreen;

                portraitMultiplayerImages = portraitTwoPlayerImages;
                landscapeMultiplayerImages = landscapeTwoPlayerImages;

                portraitMultiplayerWinnerImages = portraitTwoPlayerWinnerImages;
                landscapeMultiplayerWinnerImages = landscapeTwoPlayerWinnerImages;

                portraitMultiplayerImageTexts = portraitTwoPlayerImageTexts;
                landscapeMultiplayerImageTexts = landscapeTwoPlayerImageTexts;
                break;
            case 3:
                portraitMultiplayerFinalScoreScreen = portraitThreePlayerFinalScoreScreen;
                landscapeMultiplayerFinalScoreScreen = landscapeThreePlayerFinalScoreScreen;

                portraitMultiplayerImages = portraitThreePlayerImages;
                landscapeMultiplayerImages = landscapeThreePlayerImages;

                portraitMultiplayerWinnerImages = portraitThreePlayerWinnerImages;
                landscapeMultiplayerWinnerImages = landscapeThreePlayerWinnerImages;

                portraitMultiplayerImageTexts = portraitThreePlayerImageTexts;
                landscapeMultiplayerImageTexts = landscapeThreePlayerImageTexts;
                break;
            case 4:
                portraitMultiplayerFinalScoreScreen = portraitFourPlayerFinalScoreScreen;
                landscapeMultiplayerFinalScoreScreen = landscapeFourPlayerFinalScoreScreen;

                portraitMultiplayerImages = portraitFourPlayerImages;
                landscapeMultiplayerImages = landscapeFourPlayerImages;

                portraitMultiplayerWinnerImages = portraitFourPlayerWinnerImages;
                landscapeMultiplayerWinnerImages = landscapeFourPlayerWinnerImages;

                portraitMultiplayerImageTexts = portraitFourPlayerImageTexts;
                landscapeMultiplayerImageTexts = landscapeFourPlayerImageTexts;
                break;
        }

        scores = new int[numberPlayersInGame];
        for (int i = 0; i < numberPlayersInGame; i++)
        {
            scores[i] = 0;
            if (numberPlayersInGame > 1)
            {
                portraitMultiplayerImages[i].SetActive(false);
                landscapeMultiplayerImages[i].SetActive(false);
            }
        }

        unansweredQuestions = allQuestions.Take(numberQuestionsInGame).ToList();

        currentPlayer = 0;
        SetCurrentQuestion();
    }

    private void Update()
    {
        utils.CheckIfOrientationUpdated(portraitCanvas, landscapeCanvas, false);
    }

    void SetCurrentQuestion()
    {
        portraitNumberPlayerText.text = "Player " + (currentPlayer + 1);
        landscapeNumberPlayerText.text = "Player " + (currentPlayer + 1);

        randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        portraitQuestionText.text = currentQuestion.questionText;
        landscapeQuestionText.text = currentQuestion.questionText;


        randomPermutationIndex = Random.Range(0, randomAnswerPermutations.GetLength(0));

        for (int cellIndex = 0; cellIndex <= 3; cellIndex++)
        {
            int answerIndex = randomAnswerPermutations[randomPermutationIndex, cellIndex];
            portraitAnswerText[cellIndex].text = currentQuestion.answers[answerIndex];
            landscapeAnswerText[cellIndex].text = currentQuestion.answers[answerIndex];

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
            portraitMainQuestionScreen.SetActive(false);
            landscapeMainQuestionScreen.SetActive(false);

            if (numberPlayersInGame == 1)
            {
                SingleplayerEndScreen();
            }
            else
            {
                MultiplayerEndScreen();
            }
        }
        else
        {
            currentPlayer = (currentPlayer + 1) % numberPlayersInGame;
            SetCurrentQuestion();
        }
        // Fade into the next question
        yield return new WaitForSeconds(questionTransitionTime / 2);

        correctAnswerUI.SetActive(false);
        incorrectAnswerUI.SetActive(false);
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
            incorrectAnswerUI.SetActive(true);

            incorrectAnswerInfoText.text = currentQuestion.info;
        }

        StartCoroutine(TransitionToNextQuestion());
    }

    void SingleplayerEndScreen()
    {
        if (Mathf.RoundToInt(100 * scores[0] / numberQuestionsPerPlayer) > highScore)  // THIS IS WRONG - highPercentSc
        {
            highScore = Mathf.RoundToInt(100 * scores[0] / numberQuestionsPerPlayer);
            PlayerPrefs.SetInt("highScore", highScore);
        }

        portraitSingleplayerFinalScoreScreen.SetActive(true);
        landscapeSingleplayerFinalScoreScreen.SetActive(true);

        portraitSingleplayerScoreText.text = "Your final score is: " + Mathf.RoundToInt(100 * scores[0] / numberQuestionsPerPlayer) + "%";
        landscapeSingleplayerScoreText.text = "Your final score is: " + Mathf.RoundToInt(100 * scores[0] / numberQuestionsPerPlayer) + "%";

        portraitSingleplayerHighScoreText.text = "Your highest score is: " + highScore + "%";
        landscapeSingleplayerHighScoreText.text = "Your highest score is: " + highScore + "%";
    }

    void MultiplayerEndScreen()
    {
        portraitMultiplayerFinalScoreScreen.SetActive(true);
        landscapeMultiplayerFinalScoreScreen.SetActive(true);

        int winningScore = scores.Max();

        for (int i = 0; i < numberPlayersInGame; i++)
        {
            portraitMultiplayerImages[i].SetActive(true);
            landscapeMultiplayerImages[i].SetActive(true);

            portraitMultiplayerWinnerImages[i].SetActive(false);
            landscapeMultiplayerWinnerImages[i].SetActive(false);

            portraitMultiplayerImages[i].GetComponent<Image>().color = new Color32(71, 183, 255, 255);
            landscapeMultiplayerImages[i].GetComponent<Image>().color = new Color32(71, 183, 225, 255);

            portraitMultiplayerImages[i].GetComponent<Outline>().effectColor = new Color32(16, 104, 255, 71);
            landscapeMultiplayerImages[i].GetComponent<Outline>().effectColor = new Color32(16, 104, 255, 71);

            portraitMultiplayerImageTexts[i].GetComponent<RectTransform>().offsetMin = new Vector2(50f, 50f);
            landscapeMultiplayerImageTexts[i].GetComponent<RectTransform>().offsetMin = new Vector2(50f, 50f);

            portraitMultiplayerImageTexts[i].text = "Player " + (i + 1) + "'s Final Score: " + Mathf.RoundToInt(100 * scores[i] / numberQuestionsPerPlayer) + "%";
            landscapeMultiplayerImageTexts[i].text = "Player " + (i + 1) + "'s Final Score: " + Mathf.RoundToInt(100 * scores[i] / numberQuestionsPerPlayer) + "%";

            if (scores[i] == winningScore && scores[i] != 0)
            {
                // You know that the i’th player is the winning player (there may be more than one winning player)
                portraitMultiplayerWinningImage = portraitMultiplayerImages[i].GetComponent<Image>();
                landscapeMultiplayerWinningImage = landscapeMultiplayerImages[i].GetComponent<Image>();

                portraitMultiplayerWinningImage.sprite = winningImage;
                landscapeMultiplayerWinningImage.sprite = winningImage;

                portraitMultiplayerWinningImage.color = new Color32(254, 255, 114, 255);
                landscapeMultiplayerWinningImage.color = new Color32(254, 255, 114, 255);

                portraitMultiplayerWinningImage.GetComponent<Outline>().effectColor = new Color32(255, 249, 16, 71);
                landscapeMultiplayerWinningImage.GetComponent<Outline>().effectColor = new Color32(255, 249, 16, 71);

                portraitMultiplayerImageTexts[i].GetComponent<RectTransform>().offsetMin = new Vector2(350f, 50f);
                landscapeMultiplayerImageTexts[i].GetComponent<RectTransform>().offsetMin = new Vector2(350f, 50f);

                portraitMultiplayerWinnerImages[i].SetActive(true);
                landscapeMultiplayerWinnerImages[i].SetActive(true);
            }
        }
    }
}
