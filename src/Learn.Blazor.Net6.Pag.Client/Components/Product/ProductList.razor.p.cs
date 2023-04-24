namespace Learn.Blazor.Net6.Pag.Client.Components.Product;

public partial class ProductList
{
    private bool _isStreamBtnDisabled;
    protected bool IsStreamBtnDisabled
    {
        get => _isStreamBtnDisabled;
        set
        {
            _isStreamBtnDisabled = value;
            IsCancelBtnDisabled = value is false;
            StateHasChanged();
        }
    }

    private bool _isCancelBtnDisabled = true;
    protected bool IsCancelBtnDisabled
    {
        get => _isCancelBtnDisabled;
        set
        {
            _isCancelBtnDisabled = value;
            if (value) _cancelBtnDelegate = null;
        }
    }
}