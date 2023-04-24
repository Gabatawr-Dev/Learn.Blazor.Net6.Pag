using Google.Protobuf.WellKnownTypes;
using Learn.Blazor.Net6.Pag.Grpc.Product;
using Microsoft.AspNetCore.Components;

namespace Learn.Blazor.Net6.Pag.Client.Components.Product;

public sealed partial class ProductCollection
{
    [Inject] private ProductGrpcService.ProductGrpcServiceClient _grpcClient { get; set; } = null!;

    private ButtonViewModel _streamButtonContext { get; set; } = null!;
    private ButtonViewModel _cancelButtonContext { get; set; } = null!;
    private Action? _cancelButtonDelegate;

    protected override async Task OnInitializedAsync()
    {
        InitButtons();
        Collection = new();
        
        var result = await _grpcClient.GetAllReqAsync(new Empty());
        foreach (var model in result.Units.Select(unit => unit.MapToModel())) 
            Collection.Add(model);
    }

    private void InitButtons()
    {
        ButtonContext = _streamButtonContext = new()
        {
            Class = "border-success",
            Icon = "fa-regular fa-circle-play",
            Text = "Stream",
            Command = StreamBtnCommand
        };
        _cancelButtonContext = new()
        {
            Class = "border-warning",
            Icon = "fa-solid fa-rotate-right fa-spin",
            Text = "Cancel",
            Command = CancelBtnCommand
        };
    }

    private void UpdateButton(ButtonViewModel context)
    {
        ButtonContext = context;
        StateHasChanged();
    }

    private CancellationToken GetStreamToken()
    {
        var cts = new CancellationTokenSource();
        _cancelButtonDelegate = () => cts.Cancel();
        return cts.Token;
    }
}