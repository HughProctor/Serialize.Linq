using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace Serialize.Linq.LayrCakeCustom
{
    public class ExternalNamespace
    {
        public string NameSpace { get; set; }
        public string SuffixRemove { get; set; }
        public string SuffixReplace { get; set; }
    }

	public static class TypeResolver_Helper
	{
        public static List<ExternalNamespace> GetNamespaces()
        {
            List<ExternalNamespace> namespaceList = new List<ExternalNamespace>();
            try
            {
                var assembly = Assembly.GetEntryAssembly() == null ? Assembly.GetExecutingAssembly() : Assembly.GetEntryAssembly();
                var config = ConfigurationManager.OpenExeConfiguration(assembly.Location);
                if (config == null)
                {
                    // HP - Removed due to causing too many issues
                    //throw new ConfigurationErrorsException("Failed to load configuration section.");
                    return namespaceList;
                }
                var expressionSection = config.GetSection("expressionSettings") as ExpressionConfigurationSection;

                if (expressionSection == null)
                {
                    // HP - Removed due to causing too many issues
                    //throw new ConfigurationErrorsException("Failed to load 'expressionSettings' configuration section.");
                    return namespaceList;
                }
                foreach (ExpressionConfigurationElement expressionElement in ((ExpressionConfigurationSection)expressionSection).Expressions)
                {
                    namespaceList.Add(new ExternalNamespace
                        {
                            NameSpace = expressionElement.LookupNamespace,
                            SuffixRemove = expressionElement.SuffixRemove,
                            SuffixReplace = expressionElement.SuffixReplace
                        }
                    );
                }
            }
            catch (Exception exc)
            {
                throw new Exception("Serialize.Linq error opening ExpressionSerialise Config Settings", exc);
            }
            return namespaceList;
        }
    }
}
