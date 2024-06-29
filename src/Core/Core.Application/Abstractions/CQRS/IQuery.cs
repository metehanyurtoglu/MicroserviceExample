﻿using MediatR;

namespace Core.Application.Abstractions.CQRS
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
        where TResponse : notnull
    {
    }
}
