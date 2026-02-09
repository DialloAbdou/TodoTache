using FluentValidation;
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
               [FromServices] ITacheService _tacheService,
               [FromHeader] string tokenUser
            )
        {
            var isExist = await  _tacheService.USerIsExist(tokenUser);
            if (!isExist )
            {
                return Results.BadRequest("token invalide");
            }
            var taches = await _tacheService.GetAllTacheAsync();
            return Results.Ok(taches);
        }

        private static async Task<IResult> AddTacheAsync
            (
              [FromBody] TacheImput tacheImput,
              [FromServices] ITacheService _tacheService,
              [FromServices] IValidator<TacheImput> _validator,
              [FromHeader] String tokenUSer
            )
        {
            var _result = _validator.Validate(tacheImput);
            var isExist = await _tacheService.USerIsExist(tokenUSer);

            if (!_result.IsValid || !isExist)
            {
                return Results.BadRequest(_result.Errors.Select(e => new
                {
                    e.ErrorMessage,
                    e.PropertyName

                }));
            }
           
            var tacheout = await _tacheService.AddTacheAsync(tacheImput.Titre, tokenUSer);
              return Results.Ok(tacheout);    
        }

        private  static async  Task<IResult> GetTacheById
            
            (
              [FromRoute] int id,
              [FromServices] ITacheService _tacheService,
               [FromHeader] String tokenUSer
            )
        {
            var isExist = await _tacheService.USerIsExist(tokenUSer);
            if (!isExist) return Results.BadRequest("token n'existe pas");
            var tache = await _tacheService.GeTacheById(id);
            return Results.Ok(tache);
        }

        private static async Task<IResult> UpdateTacheAsync
        (
            [FromRoute] int id,
            [FromBody] TacheImput _tacheImput,
            [FromServices] ITacheService _tacheService,
            [FromHeader] String tokenUSer,
            [FromServices] IValidator<TacheImput> _validator
         )
        {


            var resultat = _validator.Validate(_tacheImput);
            var isExist = await _tacheService.USerIsExist(tokenUSer);

            if (!resultat.IsValid || !isExist)
            {
                return Results.BadRequest(resultat.Errors.Select(e => new
                {
                    e.ErrorMessage,
                    e.PropertyName

                }));
            }
            var isupdate = await _tacheService.UpdateTacheAsync(id, _tacheImput);  
             if(isupdate)  return  Results.Ok(isupdate);
             return Results.NotFound();
           

        }
        private static async Task<IResult> DeleteTacheAsync
           (
             [FromRoute] int id,
             [FromServices] ITacheService _tacheService,
              [FromHeader] String tokenUSer

           )
        {
            var istoken = await _tacheService.USerIsExist(tokenUSer);
            if(!istoken)
            {
                return Results.BadRequest("ce token n'existe pas");
            }
            var _result =  await _tacheService.DeleteTacheAsync(id);
             if(_result) return Results.Ok(_result);
            return Results.NotFound();

        }
    }
}
