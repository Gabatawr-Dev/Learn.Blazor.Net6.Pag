using Grpc.Core;
using Learn.Blazor.Net6.Pag.Client.Services.Product;
using Learn.Blazor.Net6.Pag.Grpc.Product;
using Learn.Blazor.Net6.Pag.Models.Product;
using Microsoft.AspNetCore.Components;

namespace Learn.Blazor.Net6.Pag.Client.Components.Product;

public sealed partial class ProductCollection
{
    [Inject] private IProductService _service { get; set; } = null!;

    private const int QUANTITY_PER_PAGE = 2;
    private int _currentPage;
    private int _lastLoadedQuantity;

    private ButtonViewModel _streamButtonContext { get; set; } = null!;
    private ButtonViewModel _cancelButtonContext { get; set; } = null!;
    
    private Action? _cancelButtonDelegate;
    //private ProductGetQuantityRes? _quantityInfo;

    private ButtonViewModel LoadButtonContext { get; set; } = null!;
    private List<ProductModel> Collection { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        InitButtons();
        //_quantityInfo = await _service.GetQuantityAsync(true);
        await PageLoadingCommand();
    }

    private void InitButtons()
    {
        LoadButtonContext = _streamButtonContext = new ButtonViewModel
        {
            Class = "border-success",
            IconClass = "fa-regular fa-circle-play",
            Text = "Load",
            Command = NextPageLoadingCommand
        };
        _cancelButtonContext = new ButtonViewModel
        {
            Class = "border-warning",
            IconClass = "fa-solid fa-rotate-right fa-spin",
            Text = "Loading",
            Command = CancelBtnCommand
        };
    }

    private void UpdateButton(ButtonViewModel context)
    {
        LoadButtonContext = context;
        StateHasChanged();
        if (context == _streamButtonContext)
            _cancelButtonDelegate = null;
    }

    private CancellationToken GetToken()
    {
        var cts = new CancellationTokenSource();
        _cancelButtonDelegate = cts.Cancel;
        return cts.Token;
    }
}