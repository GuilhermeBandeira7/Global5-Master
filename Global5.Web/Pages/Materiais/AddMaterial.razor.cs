using Global5.Core.Handlers;
using Global5.Core.Requests.MaterialRequest;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Global5.Web.Pages.Materiais;

public partial class AddMaterialPage : ComponentBase
{
    public bool IsBusy { get; set; } = false;
    public CreateMaterialRequest InputModel { get; set; } = new();
    
    [Inject] 
    public IMaterialHandler Handler { get; set; } = null!;

    [Inject] //Injecting use of C# to navigate 
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
                NavigationManager.NavigateTo("/materiais");
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