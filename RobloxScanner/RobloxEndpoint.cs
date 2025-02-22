using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

// Define your structures here

// Wrapper class to hold the search results
public class RobloxSearchResponse
{
    public List<RobloxModel> data { get; set; } // Holds the list of models
    public int totalCount { get; set; } // Optional: If you need the total count for pagination
}

public class RobloxModel
{
    public long id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string author { get; set; }
    public bool isVerified { get; set; }
    public string created { get; set; }
    public string updated { get; set; }
    public bool hasScripts { get; set; }
}

public class RobloxEndpoint
{
    private static readonly HttpClient client = new HttpClient();

    // Fetches model data based on search query and page
    public static async Task<RobloxModel[]> getPage(int page, string search)
    {
        var models = new List<RobloxModel>();
        try
        {
            string url = $"https://catalog.roblox.com/v1/search/items/details?keyword={search}&page={page}&limit=10";
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var searchResponse = JsonConvert.DeserializeObject<RobloxSearchResponse>(content);

            if (searchResponse != null && searchResponse.data != null)
            {
                models.AddRange(searchResponse.data);
            }

            return models.ToArray();
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            return null;
        }
    }

    // Fetches specific model data by its ID
    public static async Task<string> getModelFromId(long modelId)
    {
        try
        {
            string url = $"https://api.roblox.com/v1/models/{modelId}";
            HttpResponseMessage response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                string errorDetails = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Error details: " + errorDetails);
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
            return null;
        }
    }


}
