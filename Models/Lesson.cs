using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyApi.Models
{
    public class Lesson
    {
        public int Id { get; set; }

        [DisplayName("Título")]
        [Required(ErrorMessage = "Campo obrigátorio")]
        public string Title { get; set; }

        [DisplayName("Descrição")]
        public string Description { get; set; }

        [DisplayName("Instrumento")]
        public string Instrument {get;set;}

        [DisplayName("Autor")]
        public string Author { get; set;}
    }
}