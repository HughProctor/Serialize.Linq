using System.Configuration;

namespace Serialize.Linq.LayrCakeCustom
{
    /// <summary>
    /// Represents a service configuration section in a configuration file.
    /// </summary>
    public class ExpressionConfigurationSection : ConfigurationSection
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionConfigurationSection"/> class.
        /// </summary>
        public ExpressionConfigurationSection()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the services.
        /// </summary>
        /// <value>The services.</value>
        [ConfigurationProperty("expressions", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ExpressionElementCollection), AddItemName = "add", ClearItemsName = "clear", RemoveItemName = "remove")]
        public ExpressionElementCollection Expressions
        {
            get
            {
                var expressionCollection = (ExpressionElementCollection)base["expressions"];
                return expressionCollection;
            }
        }

        #endregion Properties
    }
}
