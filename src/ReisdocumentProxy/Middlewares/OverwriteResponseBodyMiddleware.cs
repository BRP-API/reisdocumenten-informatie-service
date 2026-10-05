using Brp.Shared.Infrastructure.Http;
using Brp.Shared.Infrastructure.ProblemDetails;
using Brp.Shared.Infrastructure.Stream;
using Brp.Shared.Infrastructure.Validatie;
using HaalCentraal.ReisdocumentProxy.Generated;
using Newtonsoft.Json;
using ReisdocumentProxy.Helpers;
using Serilog;
using FluentValidation.Results;

namespace ReisdocumentProxy.Middlewares;

public class OverwriteResponseBodyMiddleware
{
    private readonly RequestDelegate _next;
    //private readonly IMapper _mapper;
    private readonly IDiagnosticContext _diagnosticContext;

    public OverwriteResponseBodyMiddleware(RequestDelegate next, IDiagnosticContext diagnosticContext)
    {
        _next = next;
       _diagnosticContext = diagnosticContext;
    }

    public async Task Invoke(HttpContext context)
    {
        var orgBodyStream = context.Response.Body;
        MemoryStream? newBodyStream = null;

        try
        {
            if (!await context.HandleRequestMethodIsAllowed())
            {
                return;
            }
            if (!await context.HandleRequestAcceptIsSupported())
            {
                return;
            }
            if (!await context.HandleMediaTypeIsSupported())
            {
                return;
            }

            var requestBody = await context.Request.ReadBodyAsync();

            if (!await context.HandleRequestBodyIsValidJson(requestBody, new DummyRequestBodyValidationService()))
            {
                return;
            }

            ReisdocumentenQuery? reisdocumentenQuery = JsonConvert.DeserializeObject<ReisdocumentenQuery>(requestBody);

           // using var newBodyStream = new MemoryStream();
            newBodyStream = new MemoryStream();
            context.Response.Body = newBodyStream;
            await _next(context);

            if (!await context.HandleNotFound())
            {
                return;
            }
            if (!await context.HandleServiceIsAvailable())
            {
                return;
            }

            var body = await context.Response.ReadBodyAsync();

            if (Log.IsEnabled(Serilog.Events.LogEventLevel.Debug))
            {
                _diagnosticContext.Set("original response headers", context.Response.Headers);
                _diagnosticContext.Set("original response body", body);
            }

            var modifiedBody = context.Response.StatusCode == StatusCodes.Status200OK
                ? body.Transform(reisdocumentenQuery!.Fields!)
                : body;

            if (Log.IsEnabled(Serilog.Events.LogEventLevel.Debug))
            {
                _diagnosticContext.Set("transformed responseBody", modifiedBody);
            }

            using var bodyStream = modifiedBody.ToMemoryStream(context.Response.UseGzip());

            context.Response.ContentLength = bodyStream.Length;
            await bodyStream.CopyToAsync(orgBodyStream);
        }
        catch (Exception ex)
        {
            _diagnosticContext.SetException(ex);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.HandleInternalServerError();
        }
         finally
        {
            // GEGARANDEERD: De originele stream wordt ALTIJD hersteld, 
            // ook bij fouten, exceptions of vroege 'return' statements.
            context.Response.Body = orgBodyStream;

            if (newBodyStream != null)
            {
                // Als er succesvol data is weggeschreven in de try, zet het dan over naar de echte client-stream
                if (context.Response.StatusCode == StatusCodes.Status200OK && newBodyStream.Length > 0)
                {
                    newBodyStream.Position = 0;
                    await newBodyStream.CopyToAsync(orgBodyStream);
                }
                newBodyStream.Dispose();
            }
        }
    }

    internal class DummyRequestBodyValidationService : IRequestBodyValidator
    {
        public ValidationResult ValidateRequestBody(string requestBody)
        {
            return new ValidationResult();
        }
    }
}
