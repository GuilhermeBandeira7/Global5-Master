using Global5.Core.Handlers;
using Global5.Core.Requests.FornecedorRequest;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Global5.Web.Pages.Fornecedores;

public partial class AddFornecedorPage : ComponentBase
{
    public bool IsBusy { get; set; } = false;
    public CreateFornecedorRequest InputModel { get; set; } = new();
    
    
    [Inject] 
    public IFornecedorHandler Handler { get; set; } = null!;

    [Inject] 
    public NavigationManager NavigationManager { get; set; } = null!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    
    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {

            var result = await Handler.CreateAsync(InputModel);
            if (result.IsSuccess)
            {
                Snackbar.Add(result.Message, Severity.Success);
                NavigationManager.NavigateTo("/fornecedores");
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