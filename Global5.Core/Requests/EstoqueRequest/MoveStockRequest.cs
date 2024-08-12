using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace Global5.Core.Requests.EstoqueRequest;

public class MoveStockRequest : Request
{
    public DateTime DateTime { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "É necessário informar um fornecedor")]
    public int FornecedorId { get; set; }
    
    [Required(ErrorMessage = "É necessário informar um material")]
    public int MaterialId { get; set; }
    
    [Required(ErrorMessage = "A quantidade deve ser maior que zero")]
    public int Quantidade { get; set; }

    [DataType(DataType.Currency)]
    [DisplayFormat(DataFormatString = "{0:C0}")]
    public decimal ValorTotal { get; set; }
    
    [Required(ErrorMessage = "Tipo de Operação inválida")]
    [MaxLength(20, ErrorMessage = "O Tipo de operação deve conter até 20 caracteres")]
    public string TipoOperacao { get; set; } = string.Empty;
}