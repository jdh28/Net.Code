using System.Collections.Generic;
using System.Linq;

namespace LumenWorks.Framework.IO.Csv
{
    class CsvHeader : CsvLine
    {
        private readonly Dictionary<string, int> _fieldHeaderIndexes;

        public CsvHeader(IEnumerable<string> fields, string defaultHeaderName)
            : base(DefaultWhereEmpty(fields, defaultHeaderName))
        {
            _fieldHeaderIndexes = Fields.Select((f, i) => new {f, i}).ToDictionary(x => x.f, x => x.i);
        }

        private static IEnumerable<string> DefaultWhereEmpty(IEnumerable<string> fields, string defaultHeaderName)
        {
            var realFields = fields.Select((f, i) => string.IsNullOrWhiteSpace(f) ? defaultHeaderName + i : f).ToList();
            return realFields;
        }

        public int this[string headerName]
        {
            get { return _fieldHeaderIndexes[headerName]; }
        }

        public bool TryGetIndex(string name, out int index)
        {
            return _fieldHeaderIndexes.TryGetValue(name, out index);
        }
    }
}