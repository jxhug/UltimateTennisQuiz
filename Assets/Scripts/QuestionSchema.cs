using System;
using System.Collections.Generic;

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
