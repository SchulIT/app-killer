using CommandLine;
using System.Collections.Generic;

namespace AppKiller
{
    public class Options
    {
        [Option('t', "time", Required = false, Default = 5, HelpText = "Legt fest, wie lange gewartet wird bevor erneut gestartete Prozesse gesucht werden.")]
        public int WaitingTime { get; set; }

        [Option('a', "apps", Required = true, HelpText = "Name der zu schließenden Prozesse.")]
        public IEnumerable<string> Applications { get; set; }
    }
}
