

namespace administracion.BussinesLogic.Commands
{
    public abstract class Command<TOut> : ICommand<TOut>
    {
        public abstract void Execute();

        public abstract TOut GetResult();
    }
}
