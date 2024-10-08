using System.ComponentModel.DataAnnotations;

namespace Global5.Core.Requests.MaterialRequest;

public class GetMaterialRequest : Request
{
    [Required(ErrorMessage = "É necessário informar o Id do Produto")]
    public int Id { get; set; }
}