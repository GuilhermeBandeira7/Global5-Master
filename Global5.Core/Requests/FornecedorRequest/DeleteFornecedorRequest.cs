using System.ComponentModel.DataAnnotations;

namespace Global5.Core.Requests.FornecedorRequest;

public class DeleteFornecedorRequest : Request
{
    [Required(ErrorMessage = "É necessário informar o Id do Fornecedor")]
    public int Id { get; set; }
}