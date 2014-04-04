
using System;
namespace Protell.Model
{
    public class LinksModel:ModelBase
    {
        public long IdLink
        {
            get { return _IdLink; }
            set
            {
                if (_IdLink != value)
                {
                    _IdLink = value;
                    OnPropertyChanged(IdLinkPropertyName);
                }
            }
        }
        private long _IdLink;
        public const string IdLinkPropertyName = "IdLink";

        public string LinkUrl
        {
            get { return _LinkUrl; }
            set
            {
                if (_LinkUrl != value)
                {
                    _LinkUrl = value;
                    OnPropertyChanged(LinkUrlPropertyName);
                }
            }
        }
        private string _LinkUrl;
        public const string LinkUrlPropertyName = "LinkUrl";

        public string LinkName
        {
            get { return _LinkName; }
            set
            {
                if (_LinkName != value)
                {
                    _LinkName = value;
                    OnPropertyChanged(LinkNamePropertyName);
                }
            }
        }
        private string _LinkName;
        public const string LinkNamePropertyName = "LinkName";

        public bool IsActive
        {
            get { return _IsActive; }
            set
            {
                if (_IsActive != value)
                {
                    _IsActive = value;
                    OnPropertyChanged(IsActivePropertyName);
                }
            }
        }
        private bool _IsActive;
        public const string IsActivePropertyName = "IsActive";

        public long LastModifiedDate
        {
            get { return _LastModifiedDate; }
            set
            {
                if (_LastModifiedDate != value)
                {
                    _LastModifiedDate = value;
                    OnPropertyChanged(LastModifiedDatePropertyName);
                }
            }
        }
        private long _LastModifiedDate;
        public const string LastModifiedDatePropertyName = "LastModifiedDate";

        public bool IsModified
        {
            get { return _IsModified; }
            set
            {
                if (_IsModified != value)
                {
                    _IsModified = value;
                    OnPropertyChanged(IsModifiedPropertyName);
                }
            }
        }
        private bool _IsModified;
        public const string IsModifiedPropertyName = "IsModified";

        public Nullable<long> ServerLastModifiedDate
        {
            get { return _ServerLastModifiedDate; }
            set
            {
                if (_ServerLastModifiedDate != value)
                {
                    _ServerLastModifiedDate = value;
                    OnPropertyChanged(ServerLastModifiedDatePropertyName);
                }
            }
        }
        private Nullable<long> _ServerLastModifiedDate;
        public const string ServerLastModifiedDatePropertyName = "ServerLastModifiedDate";
    }
}
