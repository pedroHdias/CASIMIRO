using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TennisApp.Models
{
    public class Noticia
    {
        public Noticia()
        {
            this.Categorias = new HashSet<Categoria>();
            this.Comentarios = new HashSet<Comentario>();
        }

        [Key]
        public int IdNoticia { get; set; }

        [Required(ErrorMessage = "É necessário um {0}.")]
        [DisplayName("Título")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "O {0} deve ter entre {2} e {1} caracteres.")]
        public string Titulo { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "É necessária uma {0}.")]
        [StringLength(10000, MinimumLength = 10, ErrorMessage = "A {0} deve ter entre {2} e {1} caracteres.")]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "É necessário referenciar uma Imagem.")]
        public string Foto { get; set; }

        // identifica se estamos perante um ficheiro ou um URL
        public string TipoImagem { get; set; }

        [Required(ErrorMessage = "É necessário preencher {0}.")]
        [DisplayName("Relevância")]
        [Range(1, 5, ErrorMessage = "A {0} deve ser entre {2} e {1} caracteres.")]
        public int Relevancia { get; set; }


        //[Required(ErrorMessage = "É necessário preencher Data de Publicação.")]
        //[DisplayName("Data de Publicação")]
        [DataType(DataType.Date)]
        public DateTime DataPublicacao { get; set; }


        //[Required(ErrorMessage = "É necessário preencher Data de Publicação.")]
        //[DisplayName("Data de Publicação")]
        [DataType(DataType.Date)]
        public DateTime DataLimiteVisualizacao { get; set; }

        public bool Visivel { get; set; }
        
        // criador da Notícia
        [ForeignKey("Criador")]
        public int CriadorFK { get; set; }
        public virtual Utilizador Criador { get; set; }

        public virtual ICollection<Categoria> Categorias { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }

    }
}