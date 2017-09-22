using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TennisApp.Models
{
    public class Comentario
    {
        [Key]
        public int IdComentario { get; set; }

        [Required(ErrorMessage = "É necessário preencher o {0}.")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "O {0} deve ter entre {2} e {1} caracteres.")]
        public string Texto { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataComentario { get; set; }
        
        // tornar comentario visivel ou nao
        public bool Visivel { get; set; }
        
        // um Comentario pertence a UMA Notícia
        [ForeignKey("Noticia")]
        [DisplayName("Notícia")]
        public int NoticiaFK { get; set; }
        public virtual Noticia Noticia        { get; set; }

        // pessoa que coloca o Comentario
        [ForeignKey("Criador")]
        [DisplayName("Autor")]
        public int CriadorFK { get; set; }
        public virtual Utilizador Criador { get; set; }

    }
}