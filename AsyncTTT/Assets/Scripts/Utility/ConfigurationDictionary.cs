using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.Utility
{
    public class ConfigurationDictionary : IReadOnlyDictionary<string, object>
    {
        private readonly IReadOnlyDictionary<string, object> _innerDictionary;

        public ConfigurationDictionary()
        {
            _innerDictionary = new Dictionary<string, object>();
        }

        public ConfigurationDictionary(IReadOnlyDictionary<string, object> dictionary)
        {
            _innerDictionary = dictionary;
        }

        public object this[string key] { get => _innerDictionary.ContainsKey(key) ? _innerDictionary[key] : null; }

        public bool ContainsKey(string key)
        {
            return this[key] != null;
        }

        public IEnumerable<string> Keys => _innerDictionary.Keys;
        public IEnumerable<object> Values => _innerDictionary.Values;
        public int Count => _innerDictionary.Count;
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _innerDictionary.GetEnumerator();
        }
        public bool TryGetValue(string key, out object value)
        {
            return _innerDictionary.TryGetValue(key, out value);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _innerDictionary.GetEnumerator();
        }
    }
}
