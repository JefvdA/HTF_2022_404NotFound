using System.Net.Http.Headers;
using System.Net.Http.Json;
using DataLayer.Enums;

namespace DataLayer;

public class ApiClient {
    
    private const string BaseUrl = "https://app-htf-2022.azurewebsites.net/api/path";
    
    public static string? BearerToken { get; set; }
    
    public static async Task<ApiResponse> GetSample(EChallengeTrack track, EChallengeDifficulty difficulty) {
        var uri = GenerateUri(track, difficulty, EChallengeType.Sample);
        var response = await GetAsync(uri);
        return response;
    }

    public static async Task<ApiResponse> PostSample<T>(EChallengeTrack track, EChallengeDifficulty difficulty, T input) {
        var uri = GenerateUri(track, difficulty, EChallengeType.Sample);
        var response = await PostAsync(uri, input);
        return response;
    }
    
    public static async Task<ApiResponse> GetPuzzle(EChallengeTrack track, EChallengeDifficulty difficulty) {
        var uri = GenerateUri(track, difficulty, EChallengeType.Puzzle);
        var response = await GetAsync(uri);
        return response;
    }

    public static async Task<ApiResponse> PostPuzzle<T>(EChallengeTrack track, EChallengeDifficulty difficulty, T input) {
        var uri = GenerateUri(track, difficulty, EChallengeType.Puzzle);
        var response = await PostAsync(uri, input);
        return response;
    }

    private static async Task<ApiResponse> GetAsync(string uri)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);
        var response = await client.GetAsync(uri);
        var content = response.Content;
        return new ApiResponse {
            StatusCode = response.StatusCode,
            Content = content
        };
    }
    
    private static async Task<ApiResponse> PostAsync<T>(string uri, T input)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);
        var response = await client.PostAsJsonAsync<T>(uri, input);
        var content = response.Content;
        return new ApiResponse {
            StatusCode = response.StatusCode,
            Content = content
        };
    }
    
    private static string GenerateUri(EChallengeTrack track, EChallengeDifficulty difficulty, EChallengeType type) {
        return BaseUrl + $"/{(int)track}/{difficulty.ToString().ToLower()}/{type}";
    }
}