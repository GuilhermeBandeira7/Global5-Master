namespace Global5.Core.Models;

public class Material
{
    public int Id { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string UnidadeMedida { get; set; } = String.Empty;

    //A prop UserId faz referência ao usuário que for manipular um produto
    public string UserId { get; set; } = string.Empty;
}