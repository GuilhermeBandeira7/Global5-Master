using System.ComponentModel.DataAnnotations;

namespace Global5.Core.Requests.RelatorioRequest;

public class GerarRelatorioRequest : Request
{
    [Required(ErrorMessage = "É necessário informar o Id do Fornecedor")]
    public int FornecedorId { get; set; }
    public int? MaterialId { get; set; }
}