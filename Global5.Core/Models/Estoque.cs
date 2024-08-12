using System.Data.SqlTypes;
using System.Security.AccessControl;

namespace Global5.Core.Models;

public class Estoque
{
    public int Id { get; set; }
    public DateTime Data { get; set; } = DateTime.Now;
    public int FornecedorId { get; set; }
    public int MaterialId { get; set; }
    public int Quantidade { get; set; }
    public decimal ValorTotal { get; set; }
    public string TipoOperacao { get; set; } = String.Empty;
}