using System.ComponentModel.DataAnnotations;

namespace Global5.Core.Requests.EstoqueRequest;

public class RemoveMovimentationRequest : Request
{
    [Required(ErrorMessage = "É necessário informar um Id válido de movimentação.")]
    public int Id { get; set; }
}