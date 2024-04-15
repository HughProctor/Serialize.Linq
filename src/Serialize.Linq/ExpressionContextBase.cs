
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using Serialize.Linq.Interfaces;
using Serialize.Linq.Nodes;
using Serialize.Linq.LayrCakeCustom;
using System.Linq;

namespace Serialize.Linq
{
    public abstract class ExpressionContextBase : IExpressionContext
    {
        private readonly ConcurrentDictionary<string, ParameterExpression> _parameterExpressions;
        private readonly ConcurrentDictionary<string, Type> _typeCache;
        internal readonly List<ExternalNamespace> _typeLayrCakeCache;

        protected ExpressionContextBase()
        {
            _parameterExpressions = new ConcurrentDictionary<string, ParameterExpression>();
            _typeCache = new ConcurrentDictionary<string, Type>();
            _typeLayrCakeCache = TypeResolver_Helper.GetNamespaces();
        }

        public bool AllowPrivateFieldAccess { get; set; }

        public virtual BindingFlags? GetBindingFlags()
        {
            if (!AllowPrivateFieldAccess)
                return null;

            return BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        }

        public virtual ParameterExpression GetParameterExpression(ParameterExpressionNode node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            var key = node.Type.Name + Environment.NewLine + node.Name;
            return _parameterExpressions.GetOrAdd(key, k => Expression.Parameter(node.Type.ToType(this), node.Name));
        }

        public virtual Type ResolveType(TypeNode node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            if (string.IsNullOrWhiteSpace(node.Name))
                return null;

            return _typeCache.GetOrAdd(node.Name, n =>
            {
                var type = Type.GetType(n);
                if (type == null)
                {
                    foreach (var assembly in GetAssemblies().Where(x => !x.GlobalAssemblyCache))
                    {
                        type = assembly.GetType(n);
                        if (type != null)
                            break;
                        foreach (var item in _typeLayrCakeCache)
                        {
                            var newTypeName = item.NameSpace + "." + n.Split('.').Last().Replace(item.SuffixRemove, item.SuffixReplace);
                            type = assembly.GetType(newTypeName);
                            if (type != null)
                                break;
                        }
                        if (type != null)
                            break;

                    }

                }
                return type;
            });
        }

        protected abstract IEnumerable<Assembly> GetAssemblies();
    }
}
