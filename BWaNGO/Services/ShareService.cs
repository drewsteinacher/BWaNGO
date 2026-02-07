using Microsoft.JSInterop;

namespace BWaNGO.Services;

public class ShareService : IShareService
{
    private readonly IJSRuntime _jsRuntime;

    public ShareService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }
    
    public Task<string> EncodeLabelsPayload(string labels)
        => _jsRuntime.InvokeAsync<string>("LZString.compressToEncodedURIComponent", labels).AsTask();
    
    public Task<string> DecodeLabelsPayload(string payload)
        => _jsRuntime.InvokeAsync<string>("LZString.decompressFromEncodedURIComponent", payload).AsTask();
}