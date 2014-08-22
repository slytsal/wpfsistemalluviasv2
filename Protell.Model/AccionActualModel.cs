using System;

namespace Protell.Model
{
    public class AccionActualModel:ModelBase
    {
        /// <summary>
        /// Comentario de carlos
        /// </summary>
        public long IdAccionActual
        {
            
            get { return _IdAccionActual; }
            set
            {
                if (_IdAccionActual != value)
                {
                    _IdAccionActual = value;
                    OnPropertyChanged(IdAccionActualPropertyName);
                }
            }
        }
        private long _IdAccionActual;
        public const string IdAccionActualPropertyName = "IdAccionActual";

        public string AccionAcualName
        {
            get { return _AccionAcualName; }
            set
            {
                if (_AccionAcualName != value)
                {
                    _AccionAcualName = value;
                    OnPropertyChanged(AccionAcualNamePropertyName);
                }
            }
        }
        private string _AccionAcualName;
        public const string AccionAcualNamePropertyName = "AccionAcualName";

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
