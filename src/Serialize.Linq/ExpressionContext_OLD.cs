
using Serialize.Linq.Interfaces;
using Serialize.Linq.Internals;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Serialize.Linq
{
    public class ExpressionContext_OLD : ExpressionContextBase
    {
        private readonly IAssemblyLoader _assemblyLoader;

        public ExpressionContext_OLD()
            : this(new DefaultAssemblyLoader()) { }

        public ExpressionContext_OLD(IAssemblyLoader assemblyLoader)
        {
            _assemblyLoader = assemblyLoader 
                ?? throw new ArgumentNullException(nameof(assemblyLoader));
        }

        protected override IEnumerable<Assembly> GetAssemblies()
        {
            return _assemblyLoader.GetAssemblies();
        }
    }
}