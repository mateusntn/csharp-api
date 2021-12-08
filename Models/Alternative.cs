namespace MyApi.Models
{
    public class Alternative
    {
        public int Id { get; set; }
        public string AlternativeText { get; set;}
        public int QuestionId {get;set;}
        public Question Question {get;set;}
    }
}