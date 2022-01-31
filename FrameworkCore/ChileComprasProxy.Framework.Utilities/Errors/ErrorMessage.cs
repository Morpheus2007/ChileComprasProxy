using System.Collections.Generic;

namespace ChileComprasProxy.Framework.Utilities.Errors
{
    public abstract class ErrorMessage
    {
        public ErrorCode Code { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public IList<string> Detail { get; set; }
        public string Instance { get; set; }
        public string Info { get; set; }
    }
}