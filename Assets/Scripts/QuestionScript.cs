using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


[Serializable]
public class Question
{
    public string QuestionText;
    public string[] Answers = new string[4];
    public int CorrectAnswerIndex;
    public string[] Tags = new string[10];
}

[Serializable]
public class QuestionList
{
    public List<Question> Questions;
}
