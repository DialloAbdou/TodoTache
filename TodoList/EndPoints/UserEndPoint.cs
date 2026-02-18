using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Net.WebSockets;
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
            groupBuilder.MapGet("{id:int}", GetUserByIdAsyn);
            groupBuilder.MapPut("{id:int}", UpdateUSerAsync);


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
                [FromBody] UserImput userImput,
                [FromServices] IUserService _userService

            )
        {
            var userOut = await _userService.AddUserAsync(userImput);
            return Results.Ok(userOut);
        }

        private static async Task<IResult> GetUserByIdAsyn
            (
              [FromRoute] int id,
              [FromServices] IUserService _userService,
              [FromServices] IValidator<UserImput> validator, 
              [FromServices] IDistributedCache _cache
            )
        {
            var _user = await _cache.GetAsync<UserOutput>($"user_{id}");
            if (_user is null)
            {
                UserOutput? user = await _userService.GetUserByIdAsync(id);
                return Results.Ok(user);
            }
            return Results.Ok(_user);
        }

        private static  async Task<IResult> UpdateUSerAsync
            (
               [FromRoute] int id,
               [FromBody] UserImput userImput,
               [FromServices] IUserService _userService,
               [FromServices] IValidator<UserImput> validator,
               [FromServices] IDistributedCache _cache
            )
        {
            var result = validator.Validate(userImput);
            if(!result.IsValid)
            {
                return Results.BadRequest(result.Errors.Select(e => new
                {
                     e.PropertyName,
                     e.ErrorMessage

                }));
            }
            var isUpdate = await _userService.UpdateUserAsync(userImput,id);
            if (isUpdate)
            {
                await _cache.RemoveAsync($"user_{id}");
                return Results.Ok(isUpdate);
            }
            return Results.NotFound();
        }
    }
}

