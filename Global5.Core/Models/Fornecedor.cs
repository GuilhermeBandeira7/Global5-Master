namespace Global5.Core.Models;

public class Fornecedor
{
    public int Id { get; set; }
    public string CNPJ { get; set; } = string.Empty;
    public string RazaoSocial { get; set; } = string.Empty;
    
    //A propriedade UserId faz referência ao usuário que for manipular um fornecedor
    public string UserId { get; set; } = string.Empty;
}