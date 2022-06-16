using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using UserManagement.UserManager.Exceptions;
using UserManagement.UserManager.Models.DataResponse;
using UserManagement.UserManager.Models.Error;

namespace UserManagement.UserManager.Extensions;

public static class GlobalExceptionExtension
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
     {
         app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {

                int code = (int)HttpStatusCode.InternalServerError;

                context.Response.ContentType = "application/json";

                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                var exception = exceptionHandlerPathFeature?.Error;
                

                switch (exception)
                {
                    case BadRequestException e:
                        code = (int)HttpStatusCode.BadRequest;
                        break;
                    case NotFoundException e:
                        code = (int)HttpStatusCode.NotFound;
                        break;
                    case UnAuthenticationException e:
                        code = (int)HttpStatusCode.Unauthorized;
                        break;
                    case ForbiddenException e:
                        code = (int)HttpStatusCode.Forbidden;
                        break;
                }

                context.Response.StatusCode = code;

                var errorResponse = new ErrorDetail(exception!,code, DateTime.Now);


                var response = new DataResponse(false, null!, errorResponse);

                await context.Response.WriteAsync(response.ToString()!);
            });
        });
     }
}