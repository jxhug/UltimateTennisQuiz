using UnityEngine;

public class Question
{
    public string QuestionText;
    public string[] Answers = new string[4];
    public int CorrectAnswerIndex;
    public string[] Tags = new string[10];

    public static Question CreateFromJSON()
    {
        var jsonTextFile = Resources.Load<TextAsset>("TestQuestion");
        return JsonUtility.FromJson<Question>(jsonTextFile.text);
    }
}
