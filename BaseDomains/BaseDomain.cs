using System;
using BaseReadModels;
using Common;
using Extensions;

namespace BaseDomains
{
    public abstract class BaseDomain
    {
        public long NumericalOrder { get; protected set; }
        public string Id => Code;
        public string Code { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public DateTime CreateDateUtc { get; protected set; }
        public string CreatedUid { get; protected set; }
        public DateTime UpdatedDate { get; protected set; }
        public DateTime UpdatedDateUtc { get; protected set; }
        public string UpdatedUid { get; protected set; }
        public string LoginUid { get; protected set; }
        public int Version { get; protected set; }

        public BaseDomain()
        {
            Code = CommonUtility.GenerateGuid();
            CreatedUid = string.Empty;
            CreatedDate = Extension.GetCurrentDate();
            CreateDateUtc = Extension.GetCurrentDateUtc();
            UpdatedUid = CreatedUid;
            UpdatedDate = CreatedDate;
            UpdatedDateUtc = CreateDateUtc;
            Version = 0;
        }

        public BaseDomain(BaseReadModel model)
        {
            if (model == null) return;
            NumericalOrder = model.NumericalOlder;
            Code = model.Code;
            CreatedDate = model.CreatedDate;
            CreateDateUtc = model.CreatedDateUtc;
            CreatedUid = model.CreatedUid;
            UpdatedDate = model.UpdatedDate;
            UpdatedDateUtc = model.UpdatedDateUtc;
            UpdatedUid = model.UpdatedUid;
            Version = model.Version;
            LoginUid = model.LoginUid;
        }
    }
}