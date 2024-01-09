﻿using MediatR;

namespace GoogleKeep.Api.Commands
{
    public interface ICommand : IRequest { }

    public interface ICommand<TResult> : IRequest<TResult> { }
}
