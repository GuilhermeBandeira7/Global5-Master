using Global5.Core.Handlers;
using Global5.Core.Requests.RelatorioRequest;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Global5.Web.Pages.Relatorio;

public partial class RelatorioPage : ComponentBase
{
    public List<Core.Models.Relatorio> Relatorio { get; set; } = [];
    
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    
    [Inject]
    public IDialogService Dialog { get; set; } = null!;

    [Inject]
    public IRelatorioHandler Handler { get; set; } = null!;
    
    
    public async Task OnFormSubmit(int fornecedorId, int matarialId)
    {
        try
        {
            var request = new GerarRelatorioRequest()
            {
                FornecedorId = fornecedorId,
                MaterialId = matarialId
            };
            var response = await Handler.GenerateAsync(request);
            if (response.IsSuccess)
                Relatorio = response.Data ??[];
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
}