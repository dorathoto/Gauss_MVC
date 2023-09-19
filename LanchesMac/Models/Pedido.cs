using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models;

public class Pedido
{
    public int PedidoId { get; set; }

    [Required(ErrorMessage = "Informe o nome")]
    [MaxLength(50)]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Informe o sobrenome")]
    [MaxLength(50)]
    public string Sobrenome { get; set; }

    [Required(ErrorMessage = "Informe o seu endereço")]
    [MaxLength(100)]
    [Display(Name = "Endereço")]
    public string Endereco1 { get; set; }

    [MaxLength(100)]
    [Display(Name = "Complemento")]
    public string Endereco2 { get; set; }

    [Required(ErrorMessage = "Informe o seu CEP")]
    [Display(Name = "CEP")]
    [StringLength(10, MinimumLength = 8)]
    public string Cep { get; set; }

    [MaxLength(10)]
    public string Estado { get; set; }

    [StringLength(50)]
    public string Cidade { get; set; }

    [Required(ErrorMessage = "Informe o seu telefone")]
    [StringLength(25)]
    [DataType(DataType.PhoneNumber)]
    public string Telefone { get; set; }

    [Required(ErrorMessage = "Informe o email.")]
    [MaxLength(50)]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])",
        ErrorMessage = "O email não possui um formato correto")]
    public string Email { get; set; }

    [ScaffoldColumn(false)]
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Total do Pedido")]
    public decimal PedidoTotal { get; set; }

    [ScaffoldColumn(false)]
    [Display(Name = "Itens no Pedido")]
    public int TotalItensPedido { get; set; }

    [Display(Name = "Data do Pedido")]
    [DataType(DataType.Text)]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
    public DateTime PedidoEnviado { get; set; }

    [Display(Name = "Data Envio Pedido")]
    [DataType(DataType.Text)]
    [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
    public DateTime? PedidoEntregueEm { get; set; }

    public List<PedidoDetalhe> PedidoItens { get; set; }

}