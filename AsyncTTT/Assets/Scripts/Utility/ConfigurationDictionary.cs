using System.Collections.Generic;

namespace Assets.Scripts.Utility
{
    public class ConfigurationDictionary : Dictionary<string, object>
    {
        public new object this[string key]
        {
            get => base.ContainsKey(key) ? base[key] : null;
            set => base[key] = value;
        }

        public new bool ContainsKey(string key)
        {
            return this[key] != null;
        }
    }
}
