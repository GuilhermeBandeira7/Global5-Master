namespace Global5.Core.Requests;

public abstract class Request
{
    //Todas as requisições(de todos os tipos) para a API,
    //terão, obrigatoriamente, o Id do usuário
    public string UserId { get; set; } = string.Empty;
}