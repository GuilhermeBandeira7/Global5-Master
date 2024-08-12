using Global5.Core.Handlers;
using Global5.Core.Models;
using Global5.Core.Requests.MaterialRequest;
using Global5.Core.Requests;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Global5.Web.Pages.Materiais;

public partial class MateriaisPage : ComponentBase
{
    public bool IsBusy { get; set; } = false;
    public List<Material> Materiais { get; set; } = [];
    
    public Material? Material = new();
    
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;

    [Inject]
    public IDialogService Dialog { get; set; } = null!;

    [Inject]
    public IMaterialHandler Handler { get; set; } = null!;
    
    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var request = new GetAllMaterialRequest();
            var result = await Handler.GetAllAsync(request);
            if (result.IsSuccess)
                Materiais = result.Data ?? [];
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
            $"Ao prosseguir o material {nome} será removido. Deseja continuar?",
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
            var request = new DeleteMaterialRequest()
            {
                Id = id
            };
            await Handler.DeleteAsync(request);
           Materiais.RemoveAll(x => x.Id == id);
            Snackbar.Add($"Material {nome} removido", Severity.Info);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    public async void OnEditAsync(int id, Material material, string columnName)
    {
        try
        {
            var request = new EditMaterialRequest()
            {
                Id = id,
                Codigo = material.Codigo,
                Nome = material.Nome,
                UnidadeDeMedia = material.UnidadeMedida,
                ColumnName = columnName
            };
            var response = await Handler.EditAsync(request);
            if (response.IsSuccess)
                Snackbar.Add($"Material {material.Nome} alterado", Severity.Info);
            else
                Snackbar.Add($"Falha ao alterar o material", Severity.Error);

        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
}