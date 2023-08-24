using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models
{
    //[Table("Lanches")]
    public class Lanche
    {
        // [Key]
        public int LancheId { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser informado")]
        [Display(Name = "Nome do Lanche")]
        [StringLength(80, MinimumLength = 10, ErrorMessage = "O {0} deve ter no mínimo {1} e no máximo {2} caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A {0} deve ser informada")]
        [Display(Name = "Descrição do Lanche")]
        [MinLength(20, ErrorMessage = "{0} deve ter no mínimo {1} caracteres")]
        [MaxLength(200, ErrorMessage = "{0} pode exceder {1} caracteres")]
        //[StringLength(200, MinimumLength = 20, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres")]
        public string DescricaoCurta { get; set; }

        [Required(ErrorMessage = "O {0} do lanche deve ser informada")]
        [Display(Name = "Descrição detalhada do Lanche")]
        [MinLength(20, ErrorMessage = "{0} deve ter no mínimo {1} caracteres")]
        [MaxLength(200, ErrorMessage = "{0} pode exceder {1} caracteres")]
        //[StringLength(200, MinimumLength = 20, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres")]
        public string DescricaoDetalhada { get; set; }

        [Required(ErrorMessage = "Informe o {0} do lanche")]
        [Display(Name = "Preço")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(1, 999.99, ErrorMessage = "O {0} deve estar entre {1} e {2}")]
        public decimal Preco { get; set; }

        [Display(Name = "Caminho Imagem Normal")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string ImagemUrl { get; set; }

        [Display(Name = "Caminho Imagem Miniatura")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string ImagemThumbnailUrl { get; set; }

        [Display(Name = "Preferido?")]
        public bool IsLanchePreferido { get; set; }

        [Display(Name = "Estoque")]
        public bool EmEstoque { get; set; }

        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}
