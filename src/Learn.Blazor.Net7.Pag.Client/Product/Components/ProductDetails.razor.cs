using Learn.Blazor.Net7.Pag.Client.Common.Navigation;
using Learn.Blazor.Net7.Pag.Client.Product.Services.Product;
using Learn.Blazor.Net7.Pag.Models.Product;
using Microsoft.AspNetCore.Components;

namespace Learn.Blazor.Net7.Pag.Client.Product.Components;

public partial class ProductDetails
{
    [Inject] private INavigator _navigator { get; set; } = null!;
    [Inject] private IProductService _service { get; set; } = null!;

    [Parameter] public Guid Id { get; set; } = Guid.Empty;

    public ProductModel? Model { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var model = _navigator.StateContainer.GetOnce<ProductModel>();
        if (model is null)
        {
            if (Id != Guid.Empty)
            {
                model = await _service.GetByIdAsync(Id);
                if (model is not null)
                    Model = model;
                else _navigator.NavigateToBack();
            }
            else _navigator.NavigateToBack();
        }
        else Model = model;
    }
}