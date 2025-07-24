using Microsoft.JSInterop;

namespace Trial.AppFront.Helpers;

public static class IJSRuntimeExtensionMethods
{
    public static ValueTask<string> SetLocalStorage(this IJSRuntime js, string key, string content)
    {
        return js.InvokeAsync<string>("localStorage.setItem", key, content);
    }

    public static ValueTask<string> GetLocalStorage(this IJSRuntime js, string key)
    {
        return js.InvokeAsync<string>("localStorage.getItem", key);
    }

    public static ValueTask<string> RemoveLocalStorage(this IJSRuntime js, string key)
    {
        return js.InvokeAsync<string>("localStorage.removeItem", key);
    }
}