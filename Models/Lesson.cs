using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyApi.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obrigátorio")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Instrument {get;set;}
        public string Author { get; set;}
        public string Genre { get; set;}
        public string Level { get; set;}
        public string Article { get; set;}
        public string ArticleLegend { get; set;}
        public string LinkVideo { get; set;}
        public string VideoLegend { get; set; }
        public string PerformanceExercise { get; set;}
        public string ExerciseLegend { get; set; }      
        public List<Question> Questions { get; set;}
    }
}