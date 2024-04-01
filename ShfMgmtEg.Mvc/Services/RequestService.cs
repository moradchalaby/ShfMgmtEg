using System.Text.Json;
using Newtonsoft.Json;
using RestSharp;

using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ShfMgmtEg.Mvc.Services;

public class RequestService
{
    private readonly IConfiguration _configuration;
    private readonly RestClient _client;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RequestService(IConfiguration configuration )
    {
        _configuration = configuration;
        _httpContextAccessor = new HttpContextAccessor();
        var baseUrl = _configuration.GetSection("ConnectionApi").GetSection("BaseUrl").Value;
        string? accessToken = _httpContextAccessor.HttpContext?.Session.GetString("AccessToken");

        if (baseUrl != null)
        {
            var options = new RestClientOptions(baseUrl)
            {
                MaxTimeout = -1,
            };
         _client = new RestClient(options);
         _client.AddDefaultHeader("Authorization", "Bearer " + accessToken);
         
        }
    }
    
    public async Task<dynamic?> Get<T>(string route, string query="")

    {
        var request = new RestRequest(route + query)
            .AddHeader("Content-Type", "application/json");
        var res = await _client.ExecuteAsync(request);
        dynamic result = JsonConvert.DeserializeObject(res?.Content); 
        var response =  result?.ToObject<dynamic>();
        return  response;
    }
    
    public async Task<dynamic> Post<T>(string route, object body)
    {
        var request =  new RestRequest(route,Method.Post)
                .AddHeader("Content-Type", "application/json")
                .AddStringBody(JsonSerializer.Serialize(body), DataFormat.Json);
        var res = await _client.ExecuteAsync(request);
        dynamic result = JsonConvert.DeserializeObject(res.Content); 
        var response =  result.ToObject<dynamic>();
   
       
        return  response;
    }
    
    public async Task<T?> Put<T>(string route, object body)
    {
        var request = new RestRequest(route);
        request.AddJsonBody(body);
        var response = await _client.ExecutePutAsync(request);
        if (!response.IsSuccessful)
        {
            //Logic for handling unsuccessful response
        }

        var result = JsonSerializer.Deserialize<T>(response.Content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }
    
    public async Task<T?> Delete<T>(string route)
    {
        var request = new RestRequest(route);
        var response = await _client.ExecutePostAsync(request);
        if (!response.IsSuccessful)
        {
            //Logic for handling unsuccessful response
        }

        var result = JsonSerializer.Deserialize<T>(response.Content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }
    
    public async Task<T?> Patch<T>(string route, object body)
    {
        var request = new RestRequest(route);
        request.AddJsonBody(body);
        var response = await _client.ExecutePostAsync(request);
        if (!response.IsSuccessful)
        {
            //Logic for handling unsuccessful response
        }

        var result = JsonSerializer.Deserialize<T>(response.Content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result;
    }
  
}
