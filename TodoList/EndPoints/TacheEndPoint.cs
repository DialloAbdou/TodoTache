using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using TodoList.Dtos;
using TodoList.Services;

namespace TodoList.EndPoints
{
    public static class TacheEndPoint
    {
        public static RouteGroupBuilder MapTacheRouteGroup(this RouteGroupBuilder groupBuilder)
        {

            groupBuilder.MapGet("", GetAllTache);
            groupBuilder.MapGet("/{id:int}", GetTacheById);
            groupBuilder.MapPost("",AddTacheAsync);
            groupBuilder.MapPut("/{id:int}",UpdateTacheAsync);
            groupBuilder.MapDelete("/{id:int}", DeleteTacheAsync );
            return groupBuilder;
            
        }

        private static async Task<IResult> GetAllTache
            (
               [FromServices] ITacheService _tacheService
            )
        {
            var taches = await _tacheService.GetAllTacheAsync();
            return Results.Ok(taches);
        }

        private static async Task<IResult> AddTacheAsync
            (
              [FromBody] TacheImput tacheImput,
              [FromServices] ITacheService _tacheService
            )
        {
           
            var tacheout = await _tacheService.AddTacheAsync(tacheImput);
            return Results.Ok(tacheout);    
        }

        private  static async  Task<IResult> GetTacheById
            
            (
              [FromRoute] int id,
              [FromServices] ITacheService _tacheService
            )
        {
            var tache = await _tacheService.GeTacheById(id);
            return Results.Ok(tache);
        }

        private static async Task<IResult> UpdateTacheAsync
        (
            [FromRoute] int id,
            [FromBody] TacheImput _tacheImput,
            [FromServices] ITacheService _tacheService
         )
        {
            var _result = await _tacheService.UpdateTacheAsync(id, _tacheImput);  
             if(_result)  return  Results.Ok(_result);
             return Results.NotFound();

        }
        private static async Task<IResult> DeleteTacheAsync
           (
             [FromRoute] int id,
             [FromServices] ITacheService _tacheService

           )
        {
            var _result =  await _tacheService.DeleteTacheAsync(id);
             if(_result) return Results.Ok(_result);
            return Results.NotFound();

        }
    }
}
