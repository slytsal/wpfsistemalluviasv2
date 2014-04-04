using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protell.DAL.Factory
{
    public sealed class ServiceFactory
    {
        private static readonly Lazy<ServiceFactory> lazy = new Lazy<ServiceFactory>(() => new ServiceFactory());
        public static ServiceFactory Instance { get { return lazy.Value; } }


    }
}
