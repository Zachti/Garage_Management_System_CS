namespace Garage {
    internal class ActionCommand(Action i_Action) : ICommand {
        private Action Action { get; } = i_Action;

        public void Execute() {
            Action.Invoke();
        }
    }
}