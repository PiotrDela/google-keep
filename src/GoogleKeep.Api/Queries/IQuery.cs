using MediatR;

namespace GoogleKeep.Api.Queries
{
    public interface IQuery<TResult> : IRequest<TResult> { }
}
