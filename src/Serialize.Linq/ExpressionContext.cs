#region Copyright
//  Copyright, Sascha Kiefer (esskar)
//  Released under LGPL License.
//  
//  License: https://raw.github.com/esskar/Serialize.Linq/master/LICENSE
//  Contributing: https://github.com/esskar/Serialize.Linq
#endregion

using System;
using System.Reflection;
#if !WINDOWS_PHONE
using System.Collections.Concurrent;
#else
using Serialize.Linq.Internals;
#endif
using System.Linq.Expressions;
using Serialize.Linq.Nodes;
using System.Collections.Generic;
using Serialize.Linq.LayrCakeCustom;
using System.Linq;
using Serialize.Linq.Interfaces;

namespace Serialize.Linq
{
    public class ExpressionContext : IExpressionContext
    {
        private readonly ConcurrentDictionary<string, ParameterExpression> _parameterExpressions;
        private readonly ConcurrentDictionary<string, Type> _typeCache;
        internal readonly List<ExternalNamespace> _typeLayrCakeCache;

        public ExpressionContext()
        {
            _parameterExpressions = new ConcurrentDictionary<string, ParameterExpression>();
            _typeCache = new ConcurrentDictionary<string, Type>();
            _typeLayrCakeCache = TypeResolver_Helper.GetNamespaces();
        }

        public bool AllowPrivateFieldAccess { get; set; }

        public virtual BindingFlags? GetBindingFlags()
        {
            if (!this.AllowPrivateFieldAccess)
                return null;

            return BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        }

        public virtual ParameterExpression GetParameterExpression(ParameterExpressionNode node)
        {
            if(node == null)
                throw new ArgumentNullException("node");
            var key = node.Type.Name + Environment.NewLine + node.Name;
            return _parameterExpressions.GetOrAdd(key, k => Expression.Parameter(node.Type.ToType(this), node.Name));
        }

        public virtual Type ResolveType(TypeNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            if (string.IsNullOrWhiteSpace(node.Name))
                return null;

            return _typeCache.GetOrAdd(node.Name, n =>
            {
                var type = Type.GetType(n);
                if (type == null)
                {
                    foreach (var item in _typeLayrCakeCache)
                    {
                        var newTypeName = item.NameSpace + "." + n.Split('.').Last().Replace(item.SuffixRemove, item.SuffixReplace);
                        // Hugh 15/04/24
                        newTypeName = newTypeName.EndsWith(item.SuffixReplace) ? newTypeName : newTypeName + item.SuffixReplace;
                        type = FindType(newTypeName);
                        if (type != null)
                            break;
                    }

                    //foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(x => !x.GlobalAssemblyCache))
                    //{
                    //    type = assembly.GetType(n);
                    //    if (type != null)
                    //        break;
                    //    foreach (var item in _typeLayrCakeCache)
                    //    {
                    //        var newTypeName = item.NameSpace + "." + n.Split('.').Last().Replace(item.SuffixRemove, item.SuffixReplace);
                    //        // Hugh 15/04/24
                    //        newTypeName = newTypeName.EndsWith(item.SuffixReplace) ? newTypeName : newTypeName + item.SuffixReplace;
                    //        type = assembly.GetType(newTypeName);
                    //        if (type != null)
                    //            break;
                    //    }
                    //    if (type != null)
                    //        break;
                    //}

                }
                return type;
            });
        }

        /// <summary>
        /// Looks in all loaded assemblies for the given type.
        /// </summary>
        /// <param name="fullName">
        /// The full name of the type.
        /// </param>
        /// <returns>
        /// The <see cref="Type"/> found; null if not found.
        /// </returns>
        private static Type FindType(string fullName)
        {
            return
                AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => !a.GlobalAssemblyCache && !a.IsDynamic)
                    .SelectMany(a => a.GetTypes())
                    .FirstOrDefault(t => t.FullName.Equals(fullName));
        }
    }
}
