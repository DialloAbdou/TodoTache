using Microsoft.AspNetCore.Mvc;
using TodoList.Services;

namespace TodoList.EndPoints
{
    public static class UserEndPoint
    {
        public static RouteGroupBuilder MapUSerRouteGroup(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapGet("", GetAllUserAsync);
            return groupBuilder;
        }

        private static async Task<IResult> GetAllUserAsync
           (
              [FromServices] IUserService _userService
           )
        {
            var _users = await _userService.GetAllUserAsync();
            return Results.Ok(_users);
        }
    }
}
 
