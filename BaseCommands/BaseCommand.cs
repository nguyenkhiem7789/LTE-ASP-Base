using System;

namespace BaseCommands
{
    public abstract class BaseCommand
    {
        public abstract string ObjectId { get; set; }
        public abstract string ProcessUid { get; set; }
        public abstract DateTime ProcessDate { get; set; }
        public DateTime ProcessDateUtc => ProcessDate.ToUniversalTime();
        public abstract string LoginUid { get; set; }
        
        protected BaseCommand()
        {
            ObjectId = string.Empty;
            ProcessUid = string.Empty;
            ProcessDate = DateTime.Now;
        }

        protected BaseCommand(string processUid) : this()
        {
            ProcessUid = processUid;
        }

        protected BaseCommand(string objectId, string processUid) : this(objectId, processUid, DateTime.Now)
        {
            
        }

        protected BaseCommand(string objectId, string processUid, DateTime processDate)
        {
            ObjectId = objectId;
            ProcessUid = processUid;
            ProcessDate = processDate;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}