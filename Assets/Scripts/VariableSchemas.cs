using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VariableSchemaNS
{
    [Serializable]
    public class Question
    {
        public string questionText;
        public string[] answers = new string[4];
        public int correctAnswerIndex;
        public string[] tags = new string[10];
        public string info;
        public bool isActiveQuestion;
    }

    [Serializable]
    public class QuestionList
    {
        public List<Question> questions;
    }

    public class QuizScreens
    {
        //Two player end screen
        public GameObject twoPlayerFinalScoreScreen;
        public GameObject[] twoPlayerImages = new GameObject[2];
        public GameObject[] twoPlayerWinnerImages = new GameObject[2];
        public TMPro.TMP_Text[] twoPlayerImageTexts = new TMPro.TMP_Text[2];
        public Image twoPlayerWinningImage;

        //Three player end screen
        public GameObject threePlayerFinalScoreScreen;
        public GameObject[] threePlayerImages = new GameObject[3];
        public GameObject[] threePlayerWinnerImages = new GameObject[3];
        public TMPro.TMP_Text[] threePlayerImageTexts = new TMPro.TMP_Text[3];
        public Image threePlayerWinningImage;

        //Four player end screen
        public GameObject fourPlayerFinalScoreScreen;
        public GameObject[] fourPlayerImages = new GameObject[4];
        public GameObject[] fourPlayerWinnerImages = new GameObject[4];
        public TMPro.TMP_Text[] fourPlayerImageTexts = new TMPro.TMP_Text[4];
        public Image fourPlayerWinningImage;
    }
}
