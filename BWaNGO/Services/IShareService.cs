namespace BWaNGO.Services;

public interface IShareService
{
    Task<string> EncodeLabelsPayload(string labels);
    
    Task<string> DecodeLabelsPayload(string payload);
}