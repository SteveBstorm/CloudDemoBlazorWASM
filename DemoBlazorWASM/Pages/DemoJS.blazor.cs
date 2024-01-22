using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DemoBlazorWASM.Pages
{
    public partial class DemoJS
    {
        [Inject]
        public IJSRuntime jsRuntime { get; set; }

        public int MyProperty { get; set; } = 42;

        public async void SetValue()
        {
            await jsRuntime.InvokeVoidAsync("alert", MyProperty);
            int x = 0;

            await jsRuntime.InvokeVoidAsync("localStorage.setItem", "valeur", MyProperty);
            
        }

        public async void GetValue()
        {
            int x = int.Parse(await jsRuntime.InvokeAsync<string>("localStorage.getItem", "valeur"));
            await Console.Out.WriteLineAsync(x.ToString());
        }

        /*
         localStorage & sessionStorage
            .setItem(clé, valeur)
            .getItem(clé)
            .clear() => vide le storage
            .removeItem(clé)
         */
    }
}
