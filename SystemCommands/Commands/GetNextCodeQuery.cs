using BaseCommands;

namespace SystemCommands.Commands
{
    public class GetNextCodeQuery: SystemBaseCommand
    {
        public string TypeName { get; set; }
        public bool IsDigit { get; set; }
        public int Number { get; set; }
        public string Prefix { get; set; }
    }
}