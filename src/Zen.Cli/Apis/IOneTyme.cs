using System.Text.Json.Serialization;
using Refit;

namespace Zen.Cli.Apis;
public interface IOneTyme
{
    [Post("/?mode=ajax&cmd=create_note")]
    public Task<OneTymeResponse> CreateAsync([Body(BodySerializationMethod.UrlEncoded)] OneTymeRequest request);

    public static async Task<string> GetSecureLinkAsync(string note)
    {
        var onetyme = RestService.For<IOneTyme>("https://1ty.me");
        var response = await onetyme.CreateAsync(new OneTymeRequest(note));
        return response.Url;
    }
}
public record OneTymeRequest([property: JsonPropertyName("note")] string Note);
public class OneTymeResponse
{
    [JsonPropertyName("url")]
    public string? Id { get; set; }

    [JsonIgnore]
    public string Url => Id is not null ? $"https://1ty.me/{Id}" : string.Empty;
}
