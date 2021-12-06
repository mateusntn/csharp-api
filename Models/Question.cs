using System.Collections.Generic;

namespace MyApi.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set;}
        public List<Alternative> Alternatives { get; set;}
        public string CorrectAlternative { get; set;}

        public Question()
        {
        }

        public Question(int id, string questionText, List<Alternative> alternatives, string correctAlternative)
        {
            Id = id;
            QuestionText = questionText;
            Alternatives = alternatives;
            CorrectAlternative = correctAlternative;
        }
    }
}