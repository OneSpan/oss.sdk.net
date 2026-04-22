using OneSpanSign.Sdk.Internal;

namespace OneSpanSign.Sdk
{
    public interface IRestClient
    {
        string Get(string path);
        string Get(string path, string acceptType);
        string Post(string path, string jsonPayload);
        string Put(string path, string jsonPayload);
        string Delete(string path);
        string Delete(string path, string jsonPayload);
        DownloadedFile GetBytes(string path);
        DownloadedFile GetHttpAsOctetStream(string path);
        string PostMultipartFile(string path, byte[] fileBytes, string boundary, string json);
        string PostMultipartPackage(string path, byte[] content, string boundary, string json);
    }
}
