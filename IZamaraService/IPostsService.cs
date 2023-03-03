namespace Zamara.IService;
using Zamara.Models;
public interface IPostsService{
    public  Task<PostDto> GetPosts();
}