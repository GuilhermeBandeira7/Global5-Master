using System.ComponentModel.DataAnnotations;

namespace Global5.Core.Requests.FornecedorRequest;

public class GetFornecedorRequest : Request
{
    [Required(ErrorMessage = "É necessário informar o Id do Produto")]
    public int Id { get; set; }
}