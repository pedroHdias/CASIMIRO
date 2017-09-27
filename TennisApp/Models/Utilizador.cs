using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TennisApp.Models
{
    public class Utilizador
    {
        public Utilizador()
        {
            this.Noticias = new HashSet<Noticia>();
            this.Comentarios = new HashSet<Comentario>();
        }

        [Key]
        public int IdUtilizador { get; set; }

        [Required(ErrorMessage = "É necessário um {0}.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O {0} deve ter entre {2} e {1} caracteres.")]
        public string Nome { get; set; }

        //[Required(ErrorMessage = "É necessário um Tipo de Utilizador.")]
        public string Tipo { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Não é um email válido.")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber, ErrorMessage = "Não é um número de telémovel válido.")]
        [StringLength(9)]
        [RegularExpression("9[1236][0-9]{7}", ErrorMessage = "O {0} deve ter apenas 9 carateres numéricos.")]
        public string Telemovel { get; set; }

        [Required(ErrorMessage = "É necessária uma {0}.")]
        [DisplayName("Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNasc { get; set; }

        // referencia um utilizador da tabela do USERs
        //public virtual AspNetUsers AspNetUsers { get; set; }
        public string UserName { get; set; }


        public virtual ICollection<Noticia> Noticias { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }

    }
}