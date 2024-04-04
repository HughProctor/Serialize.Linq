using System.Configuration;

namespace Serialize.Linq.LayrCakeCustom
{
    /// <summary>
    /// Represents a list of service elements in a configuration file.
    /// </summary>
    public class ExpressionElementCollection : ConfigurationElementCollection
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceElementCollection"/> class.
        /// </summary>
        public ExpressionElementCollection()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the <see cref="ExpressionConfigurationElement"/> at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ExpressionConfigurationElement this[int index]
        {
            get
            {
                return (ExpressionConfigurationElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }

        /// <summary>
        /// Gets the <see cref="ExpressionConfigurationElement"/> with the specified name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        new public ExpressionConfigurationElement this[string name]
        {
            get
            {
                return (ExpressionConfigurationElement)BaseGet(name);
            }
        }

        /// <summary>
        /// Gets the type of this collection.
        /// </summary>
        /// <returns>The type of this collection.</returns>
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.AddRemoveClearMap;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Creates a new <see cref="ExpressionConfigurationElement"/>
        /// </summary>
        /// <returns>A new <see cref="ServiceConfigurationElement"/></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ExpressionConfigurationElement();
        }

        /// <summary>
        /// Gets the element key for a specified configuration element when overridden in a derived class
        /// </summary>
        /// <param name="element">The <see cref="ExpressionConfigurationElement"/> to return the key for.</param>
        /// <returns>An <see cref="T:System.Object"/> that acts as the key for the specified <see cref="ExpressionConfigurationElement"/></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ExpressionConfigurationElement)element).Name;
        }

        /// <summary>
        /// Returns the index of the specified <see cref="ExpressionConfigurationElement"/>.
        /// </summary>
        /// <param name="element">The <see cref="ExpressionConfigurationElement"/>.</param>
        /// <returns>The index of the specified <see cref="ExpressionConfigurationElement"/>.</returns>
        public int IndexOf(ExpressionConfigurationElement element)
        {
            return BaseIndexOf(element);
        }

        /// <summary>
        /// Adds the specified element.
        /// </summary>
        /// <param name="element">The <see cref="ExpressionConfigurationElement"/>.</param>
        public void Add(ExpressionConfigurationElement element)
        {
            BaseAdd(element);
        }

        /// <summary>
        /// Adds a configuration element to the <see cref="T:System.Configuration.ConfigurationElementCollection"/>.
        /// </summary>
        /// <param name="element">The <see cref="T:System.Configuration.ConfigurationElement"/> to add.</param>
        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        /// <summary>
        /// Removes the specified element.
        /// </summary>
        /// <param name="element">The <see cref="ExpressionConfigurationElement"/>.</param>
        public void Remove(ExpressionConfigurationElement element)
        {
            if (BaseIndexOf(element) >= 0)
                BaseRemove(element.Name);
        }

        /// <summary>
        /// Removes the <see cref="ExpressionConfigurationElement"/> at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        /// <summary>
        /// Removes the <see cref="ExpressionConfigurationElement"/> with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        public void Remove(string name)
        {
            BaseRemove(name);
        }

        /// <summary>
        /// Clears this collection.
        /// </summary>
        public void Clear()
        {
            BaseClear();
        }

        #endregion Methods
    }
}
