using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger) 
        : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : notnull, IRequest<TResponse> 
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handle {Request} with {RequestData}", typeof(TRequest).Name, request);

            var timer = new Stopwatch();
            timer.Start();

            var response = await next();

            timer.Stop();

            var timerTaken = timer.Elapsed;

            if(timerTaken.Seconds > 3)
            {
                logger.LogWarning("[PERFORMANCE] The request {Request} took {Seconds} seconds!", typeof(TRequest).Name, timerTaken.Seconds);
            }

            logger.LogInformation("[END] Handled {Request} with {Response}", typeof(TRequest).Name, typeof(TResponse).Name);

            return response;
        }
    }
}
