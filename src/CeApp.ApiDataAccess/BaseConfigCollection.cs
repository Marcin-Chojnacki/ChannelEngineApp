using System;
using System.Configuration;

namespace CeApp.ApiDataAccess
{
   [ConfigurationCollection(typeof(SettingEntryElement))]
    public class BaseConfigCollection : ConfigurationElementCollection
    {
        public string GetValue(string id)
        {
            var element = (SettingEntryElement)BaseGet(id);
            if (element == null)
                throw new ArgumentOutOfRangeException(nameof(id), $"Not found following configuration element: {id}");
            return element.Value;
        }

        protected override ConfigurationElement CreateNewElement() => new SettingEntryElement();

        protected override object GetElementKey(ConfigurationElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            return ((SettingEntryElement)element).Name;
        }
    }

    internal class SettingEntryElement : ConfigurationElement
    {
        [ConfigurationProperty("key", IsRequired = true)]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;'\"|\\")]
        public string Name => (string)this["key"];

        [ConfigurationProperty("value", IsRequired = true)]
        public string Value => (string)this["value"];
    }
}
