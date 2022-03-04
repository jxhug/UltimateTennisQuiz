using System.Collections.Generic;


namespace Questions
{
    public class Question
    {
        public string questionText;
        public string[] answers = new string[4];
        public int correctAnswerIndex;
        public string[] tags = new string[10];
        public string info;
        public bool isActive;
    }

    public class QuestionList
    {
        public List<Question> questions;
    }
}
