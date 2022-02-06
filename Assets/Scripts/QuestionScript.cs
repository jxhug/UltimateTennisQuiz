using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


[Serializable]
public class Question
{
    public string questionText;
    public string[] answers = new string[4];
    public int correctAnswerIndex;
    public string[] tags = new string[10];
}

[Serializable]
public class QuestionList
{
    public List<Question> questions;
}
