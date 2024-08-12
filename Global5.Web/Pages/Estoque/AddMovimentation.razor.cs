using Global5.Core.Handlers;
using Global5.Core.Requests.EstoqueRequest;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Global5.Web.Pages.Estoque;

public partial class AddMovimentationPage : ComponentBase
{
    public bool IsBusy { get; set; } = false;
    public MoveStockRequest InputModel { get; set; } = new();
    
    
    [Inject] 
    public IEstoqueHandler Handler { get; set; } = null!;

    [Inject] 
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    
    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {

            var result = await Handler.MoveAsync(InputModel);
            if (result.IsSuccess)
            {
                Snackbar.Add(result.Message, Severity.Success);
                NavigationManager.NavigateTo("/estoque");
            }
            else
                Snackbar.Add(result.Message, Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

}