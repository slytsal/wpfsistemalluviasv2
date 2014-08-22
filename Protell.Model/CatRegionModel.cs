﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.Model
{
    public class CatRegionModel:ModelBase
    {
        public long IdRegion
        {
            get { return _IdRegion; }
            set
            {
                if (_IdRegion != value)
                {
                    _IdRegion = value;
                    OnPropertyChanged(IdRegionPropertyName);
                }
            }
        }
        private long _IdRegion;
        public const string IdRegionPropertyName = "IdRegion";

        public string RegionName
        {
            get { return _RegionName; }
            set
            {
                if (_RegionName != value)
                {
                    _RegionName = value;
                    OnPropertyChanged(RegionNamePropertyName);
                }
            }
        }
        private string _RegionName;
        public const string RegionNamePropertyName = "RegionName";

        public string FileName
        {
            get { return _FileName; }
            set
            {
                if (_FileName != value)
                {
                    _FileName = value;
                    OnPropertyChanged(FileNamePropertyName);
                }
            }
        }
        private string _FileName;
        public const string FileNamePropertyName = "FileName";
    }
}
