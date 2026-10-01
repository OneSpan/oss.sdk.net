using System.Collections.Generic;
using Newtonsoft.Json;

namespace OneSpanSign.Sdk
{
    /// <summary>
    /// Result of the field overlap validation for a package. The validation is advisory: it never
    /// modifies the package and is not enforced on package creation or send.
    /// </summary>
    public class FieldOverlapValidationResult
    {
        /// <summary>
        /// One entry per field that overlaps at least one later field of the same signer on the
        /// same page; empty when no overlaps were found.
        /// </summary>
        [JsonProperty("overlaps")]
        public IList<FieldOverlap> Overlaps { get; set; } = new List<FieldOverlap>();

        [JsonIgnore]
        public bool HasOverlaps => Overlaps != null && Overlaps.Count > 0;
    }
}
