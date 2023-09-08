using Flurl.Http;
using Flurl;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace ui_tests;


public class ApiUnitTests
{

    string _instance_url = "https://ourchive-dev.stopthatimp.net/";

    [Test]
    public async Task GetWorkTypes()
    {
        var response = await "https://ourchive-dev.stopthatimp.net/".AppendPathSegments("api", "worktypes").GetAsync();
        Console.WriteLine(response.StatusCode);
        var responseBody = await response.GetJsonAsync();
        //var results = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseBody);

        var results = JsonConvert.DeserializeObject(responseBody);//.Select(x => x["type_name"]);
        var allWorkTypes = results["results"];
        foreach (var item in allWorkTypes)
        {
            Console.WriteLine(item.GetType());
            Console.WriteLine(item);
        }

    }
}