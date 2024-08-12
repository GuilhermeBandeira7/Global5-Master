using Global5.Core.Handlers;
using Microsoft.AspNetCore.Components;
using Global5.Core.Models;
using Global5.Core.Requests;
using Global5.Core.Requests.FornecedorRequest;
using MudBlazor;


namespace Global5.Web.Pages.Fornecedores;

public partial class FornecedoresPage : ComponentBase
{
    public bool IsBusy { get; set; } = false;
    public List<Fornecedor> Fornecedores { get; set; } = [];
    
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public IDialogService Dialog { get; set; } = null!;

    [Inject]
    public IFornecedorHandler Handler { get; set; } = null!;
    
    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var request = new GetAllFornecedorRequest();
            var result = await Handler.GetAllAsync(request);
            if (result.IsSuccess)
                Fornecedores = result.Data ?? [];
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
            $"Ao prosseguir o fornecedor {nome} será removido. Deseja continuar?",
            yesText: "Excluir",
            cancelText: "Cancelar");

        if (result is true)
            await OnDeleteAsync(id, nome);

        StateHasChanged();
    }

    public async Task OnDeleteAsync(int id, string nome)
    {
        try
        {
            var request = new DeleteFornecedorRequest()
            {
                Id = id
            };
            await Handler.DeleteAsync(request);
            Fornecedores.RemoveAll(x => x.Id == id);
            Snackbar.Add($"Fornecedor {nome} removido", Severity.Info);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    public async void OnEditAsync(int id, Fornecedor fornecedor)
    {
        try
        {
            var request = new EditFornecedorRequest()
            {
                Id = id,
                Cnpj = fornecedor.CNPJ,
                RazaoSocial = fornecedor.RazaoSocial
            };
            var response = await Handler.EditAsync(request);
            if (response.IsSuccess)
                Snackbar.Add($"Fornedor {fornecedor.RazaoSocial} alterado", Severity.Info);
            else
                Snackbar.Add($"Falha ao alterar o fornecedor", Severity.Error);

        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
}