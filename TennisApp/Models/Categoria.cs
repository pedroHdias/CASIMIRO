using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TennisApp.Models
{
    public class Categoria
    {
        public Categoria()
        {
            this.Noticias = new HashSet<Noticia>();
        }

        [Key]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "É necessário um {0}")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "O {0} deve ter entre {2} e {1} caracteres.")]
        [DisplayName("Nome de Categoria")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "É necessário uma {0}.")]
        [DisplayName("Descrição")]
        [StringLength(150, MinimumLength = 10, ErrorMessage = "A {0} deve ter entre {2} e {1} caracteres.")]
        public string Descricao { get; set; }
      
        public virtual ICollection<Noticia> Noticias { get; set; }
    }
}