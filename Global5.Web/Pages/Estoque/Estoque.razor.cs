
using Global5.Core.Handlers;
using Global5.Core.Requests.EstoqueRequest;
using Global5.Core.Requests.MaterialRequest;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Global5.Web.Pages.Estoque;

public partial class EstoquePage : ComponentBase
{
    public bool IsBusy { get; set; } = false;
    public List<Core.Models.Estoque> Movimentation { get; set; } = [];
    
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public IDialogService Dialog { get; set; } = null!;

    [Inject]
    public IEstoqueHandler Handler { get; set; } = null!;
    
    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var request = new GetMovimentationRequest();
            var result = await Handler.GetAsync(request);
            if (result.IsSuccess)
                Movimentation = result.Data ?? [];
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
    
    public async void OnDeleteButtonClickedAsync(int id, string nome)
    {
        var result = await Dialog.ShowMessageBox(
            "ATENÇÃO",
            $"Ao prosseguir a movimentação {nome} será removida. Deseja continuar?",
            yesText: "Excluir",
            cancelText: "Cancelar");

        if (result is true)
            await OnDeleteAsync(id);

        StateHasChanged();
    }

    public async Task OnDeleteAsync(int id)
    {
        try
        {
            var request = new RemoveMovimentationRequest()
            {
                Id = id
            };
            await Handler.RemoveAsync(request);
            Movimentation.RemoveAll(x => x.Id == id);
            Snackbar.Add($"Movimentação {id} removida", Severity.Info);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    public async void OnEditAsync(int id, Core.Models.Estoque estoque)
    {
        try
        {
            var request = new EditMovimentationRequest()
            {
                Id = id,
                Quantidade = estoque.Quantidade,
                TipoOperacao = estoque.TipoOperacao
            };
            var response = await Handler.EditAsync(request);
            if (response.IsSuccess)
                Snackbar.Add($"Movimentação {estoque.Id} alterada.", Severity.Info);
            else
                Snackbar.Add($"Falha ao alterar movimentação", Severity.Error);

        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
}