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

   [SerializeField]
   private Question CurrentQuestion;

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

      /*
      if (UnansweredQuestions == null || UnansweredQuestions.Count == 0)
      {
         UnansweredQuestions = Questions.ToList<Question>();
      }
      */

      SetCurrentQuestion();
   }
   
   void SetCurrentQuestion()  
   {
      int RandomQuestionIndex = Random.Range(0, UnansweredQuestions.Count);
      CurrentQuestion = UnansweredQuestions[RandomQuestionIndex];
      QuestionText.text = CurrentQuestion.QuestionText;

      for (int i = 0; i <= 3; i++)
      {
         AnswerText[i].text = CurrentQuestion.Answers[i];
      }
   }

   IEnumerator TransitionToNextQuestion() 
   {
      UnansweredQuestions.Remove(CurrentQuestion);

      yield return new WaitForSeconds(QuestionTransitionTime);

      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }

   public void CheckAnswer(int SelectedIndex)
   {
      if (SelectedIndex == CurrentQuestion.CorrectAnswerIndex)
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
