using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyApi.Models
{
    public class Category
    {
        public int Id { get; set; }

        [DisplayName("Categoria")]
        [Required(ErrorMessage = "Campo obrigátorio")]
        public string Name { get; set; }

        [DisplayName("Ícone")]
        public string Icon { get; set; }

        [DisplayName("Criado em")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DisplayName("Última atualização")]
        public DateTime LastUpdateDate { get; set; } = DateTime.Now;
    }
}