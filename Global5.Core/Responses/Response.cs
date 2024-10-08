using System.Text.Json.Serialization;

namespace Global5.Core.Responses;

//Classe genérica base para uma resposta
public class Response<TData>
{
    private int _code = Configuration.DefaultStatusCode;
    
    [JsonConstructor] //Define este construtor como padrão para serialização
    public Response()
        => _code = Configuration.DefaultStatusCode;
    
    public Response(
        TData? data,
        int code = Configuration.DefaultStatusCode,
        string? message = null)
    {
        Data = data;
        _code = code;
        Message = message;
    }
    
    public TData? Data { get; set; }
    public string? Message { get; set; }
    
    
    [JsonIgnore] //Ignora a propriedade na serialização
    public bool IsSuccess => _code is >= 200 and <= 299;
}