using System.Collections;

namespace paybyrd.Entities.Response
{
    public class DiffDifferencesResponse
    {
        public int Id { get; set; }
        public bool Equals { get; set; } = true;
        public string Result { get; set; }
        public IEnumerable Differences { get; set; }
    }

    public class Differences
    {
        public int startIndex { get; set; }
        public int length { get; set; }
    }
}
