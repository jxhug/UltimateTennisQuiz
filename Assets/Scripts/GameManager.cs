using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public Question[] Questions;
   private static List<Question> UnansweredQuestions;
   private Question CurrentQuestion;

   [SerializeField]
   private float QuestionTransitionTime = 1f;

   private Text QuestionText;

   private Text[] AnswerText = new Text[4];
   
   [SerializeField]
   private GameObject CorrectAnswerUI;

   [SerializeField]
   private GameObject WrongAnswerUI;

   void Start()
   {
      //var jsonTextFile = Resources.Load<TextAsset>("TestQuestion");
      //Debug.Log(jsonTextFile.text);

      CurrentQuestion = Question.CreateFromJSON();
      print(CurrentQuestion.QuestionText);
      foreach(var answer in CurrentQuestion.Answers) {
         print(answer);
      }
      print(CurrentQuestion.CorrectAnswerIndex);
      foreach(var tag in CurrentQuestion.Tags) {
         print(tag);
      }

      if (UnansweredQuestions == null || UnansweredQuestions.Count == 0)
      {
         UnansweredQuestions = Questions.ToList<Question>();
      }

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
