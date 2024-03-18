using GestaoProdutos.Domain.Commands.Contracts;

namespace GestaoProdutos.Domain.Handlers.Contracts
{
    public interface IHandler<T> where T : ICommand
    {
        Task<ICommandResult> Handle(T command);
    }
}
