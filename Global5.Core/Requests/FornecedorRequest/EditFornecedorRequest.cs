using System.ComponentModel.DataAnnotations;

namespace Global5.Core.Requests.FornecedorRequest;

public class EditFornecedorRequest : Request
{
    [Required(ErrorMessage = "É necessário informar o Id do Fornecedor")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "CNPJ inválido")]
    [MaxLength(20, ErrorMessage = "O CPNJ deve conter até 20 caracteres")]
    [MinLength(5, ErrorMessage = "O Nome deve conter no mínimo 18 caracteres")]
    public string Cnpj { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Razão Social inválida")]
    public string RazaoSocial { get; set; } = String.Empty;

    [Required(ErrorMessage = "É necessário o nome da coluna.")]
    public string ColumnName { get; set; } = String.Empty;
}