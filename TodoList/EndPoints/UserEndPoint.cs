using Microsoft.AspNetCore.Mvc;
using TodoList.Dtos;
using TodoList.Services;

namespace TodoList.EndPoints
{
    public static class UserEndPoint
    {
        public static RouteGroupBuilder MapUSerRouteGroup(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapGet("", GetAllUserAsync);
            groupBuilder.MapPost("", AddUSerAsync);
            groupBuilder.MapGet("{id:int}",GetUserByIdAsyn);
            
            return groupBuilder;
        }
        private static async Task<IResult> GetAllUserAsync
           (
              [FromServices] IUserService _userService
           )
        {
            var _users = await _userService.GetAllTacheAsync();
            return Results.Ok(_users);
        }

        private static async Task<IResult> AddUSerAsync(
                [FromBody]  UserImput userImput,
                [FromServices] IUserService _userService
            
            )
        {
             var userOut = await _userService.AddUserAsync(userImput);
               return Results.Ok(userOut);
        }

        private static async Task<IResult> GetUserByIdAsyn
            (
              [FromRoute] int id,
              [FromServices] IUserService _userService
            )
        {
             var user = await _userService.GetUserByIdAsync(id);
            return Results.Ok(user);
        }
    }
}
 
