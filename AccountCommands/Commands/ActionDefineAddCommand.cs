namespace AccountCommands.Commands
{
    public class ActionDefineAddCommand: AccountBaseCommand
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public bool IsRoot { get; set; }
        public string Id { get; set; }
    }
}