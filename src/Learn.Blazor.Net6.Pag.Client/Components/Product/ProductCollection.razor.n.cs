namespace Learn.Blazor.Net6.Pag.Client.Components.Product;

public partial class ProductCollection
{
    private class ButtonViewModel
    {
        public string Class { get; init; } = null!;
        public string Icon { get; init; } = null!;
        public string Text { get; init; } = null!;
        public Func<Task> Command { get; init; } = null!;
    }
}