namespace Learn.Blazor.Net7.Pag.Client.Product.Components;

public partial class ProductCollection
{
    private class ButtonViewModel
    {
        public string Class { get; init; } = null!;
        public string IconClass { get; init; } = null!;
        public string Text { get; init; } = null!;
        public Func<Task> Command { get; init; } = null!;
    }
}