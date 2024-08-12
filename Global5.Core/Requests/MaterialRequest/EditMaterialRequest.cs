using System.ComponentModel.DataAnnotations;

namespace Global5.Core.Requests.MaterialRequest;

public class EditMaterialRequest : Request
{
    [Required(ErrorMessage = "É necessário informar o Id do Produto.")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Nome inválido")]
    [MaxLength(20, ErrorMessage = "O Código deve conter até 20 caracteres")]
    [MinLength(5, ErrorMessage = "O Código deve conter no mínimo 5 caracteres")]
    public string Codigo { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Descrição inválida")]
    [MaxLength(50, ErrorMessage = "O Nome A descrição deve conter até 50 caracteres")]
    public string Nome { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Unidade de medida é obrigatório")]
    [MaxLength(10, ErrorMessage = "A unidade de medida deve conter até 10 caracteres")]
    public string UnidadeDeMedia { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "É necessário o nome da coluna.")]
    public string ColumnName { get; set; } = String.Empty;
}