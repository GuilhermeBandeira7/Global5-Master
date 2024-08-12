using System.ComponentModel.DataAnnotations;

namespace Global5.Core.Requests.FornecedorRequest;

public class CreateFornecedorRequest : Request
{
    [Required(ErrorMessage = "CNPJ inválido")]
    [MaxLength(20, ErrorMessage = "O CPNJ deve conter até 20 caracteres")]
    [MinLength(5, ErrorMessage = "O Nome deve conter no mínimo 18 caracteres")]
    public string CNPJ { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Razão Social inválida")]
    [MaxLength(50, ErrorMessage = "A razão social deve conter até 50 caracteres")]
    public string RazaoSocial { get; set; } = String.Empty;
}