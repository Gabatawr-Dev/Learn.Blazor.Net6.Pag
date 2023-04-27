using Learn.Blazor.Net7.Pag.Client.Common.Navigation;
using Learn.Blazor.Net7.Pag.Client.Product.Services.Product;
using Learn.Blazor.Net7.Pag.Models.Product;
using Microsoft.AspNetCore.Components;

namespace Learn.Blazor.Net7.Pag.Client.Product.Components;

public sealed partial class ProductCollection
{
    private const int QUANTITY_PER_PAGE = 5;
    
    [Inject] private INavigator _navigator { get; set; } = null!;
    [Inject] private IProductService _service { get; set; } = null!;

    [Parameter] public int Page { get; set; }
    private int _previousPage => Page - 1;

    private int _lastLoadedQuantity;

    private ButtonViewModel _streamButtonContext { get; set; } = null!;
    private ButtonViewModel _cancelButtonContext { get; set; } = null!;
    
    private Action? _cancelButtonDelegate;

    private ButtonViewModel LoadButtonContext { get; set; } = null!;
    private List<ProductModel> Collection { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        InitButtons();
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