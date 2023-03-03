using Zamara.IService;
using Zamara.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
namespace Zamara.Service;

public class PostsService : IPostsService
{
    public async Task<PostDto> GetPosts()
    {
        try
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var json = await new HttpClient().GetFromJsonAsync<PostDto>("https://dummyjson.com/posts");
            
            return json;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw ex.InnerException;
        }





    }
}