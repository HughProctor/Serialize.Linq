using System.Configuration;

namespace Serialize.Linq.LayrCakeCustom
{
    public class ExpressionConfigurationElement : ConfigurationElement
    {
        #region Constructors
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionConfigurationElement" /> class.
        /// </summary>
        public ExpressionConfigurationElement()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        /// <summary>
        /// Gets or sets the Assembly name that the Expresion TypeResolver will use to locate the object type.
        /// </summary>
        /// <value>The type.</value>
        [ConfigurationProperty("assemblyName", IsRequired = true)]
        public string ExpressionAssembly
        {
            get { return (string)this["assemblyName"]; }
            set { this["assemblyName"] = value; }
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [ConfigurationProperty("lookupNamespace", IsRequired = true)]
        public string LookupNamespace 
        {
            get { return (string)this["lookupNamespace"]; }
            set { this["lookupNamespace"] = value; }
        }

        /// <summary>
        /// Gets or sets the Suffix (Dto) of the type name that will be removed and replaced
        /// </summary>
        [ConfigurationProperty("suffixRemove", IsRequired = true)]
        public string SuffixRemove
        {
            get { return (string)this["suffixRemove"]; }
            set { this["suffixRemove"] = value; }
        }

        /// <summary>
        /// Gets or sets the Suffix (Dto) of the type name that will used in place 
        /// </summary>
        [ConfigurationProperty("suffixReplace", IsRequired = true)]
        public string SuffixReplace
        {
            get { return (string)this["suffixReplace"]; }
            set { this["suffixReplace"] = value; }
        }

        #endregion Properties
    }
}
