using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace ShfMgmtEg.Mvc.Controllers;

[Authorize]
public class ShiftsController : Controller
{
    
    // GET
    public async Task<IActionResult> Index()
    {
        var options = new RestClientOptions("https://localhost:7261")
        {
            MaxTimeout = -1,
        };
        var client = new RestClient(options);
        var request = new RestRequest("/api/Shift/5", Method.Get);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJhZG1pbiIsImVtYWlsIjoiYWRtaW5AYWRtaW4uY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbW9iaWxlcGhvbmUiOiIxMjM0NTY3ODkwIiwicm9sZSI6IkFkbWluIiwibmJmIjoxNzExOTI1OTM5LCJleHAiOjE3MTIwMTIzMzksImlhdCI6MTcxMTkyNTkzOX0.9XHJGDe0tQjQ1syMKbccjn6ygdWaJs6OU8ADaq6ZJbW2yZhuwFaooEvNEwOFCmjzjRp0auj-2o-q33KvcZG_Sg");
        var body = @"";
        request.AddStringBody(body, DataFormat.Json);
        RestResponse response = await client.ExecuteAsync(request);
        Console.WriteLine(response.Content);
        return View(response);
    }
}