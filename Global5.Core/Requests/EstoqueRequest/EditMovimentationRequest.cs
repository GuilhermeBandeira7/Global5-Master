using System.ComponentModel.DataAnnotations;

namespace Global5.Core.Requests.EstoqueRequest;

public class EditMovimentationRequest : Request
{
    [Required(ErrorMessage = "É necessário informar o Id da movimentação.")]
    public int Id { get; set; }

    public int Quantidade { get; set; }
    public string TipoOperacao { get; set; } = String.Empty;
    [Required(ErrorMessage="É necessário informar um campo para alterar")]
    public string ColumnName { get; set; } = String.Empty;
}