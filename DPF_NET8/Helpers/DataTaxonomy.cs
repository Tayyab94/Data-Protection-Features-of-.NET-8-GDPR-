using Microsoft.Extensions.Compliance.Classification;

namespace DPF_NET8.Helpers
{
    public static class DataTaxonomy
    {
        public static string TaxonomyName { get; } = typeof(DataTaxonomy).FullName!;

        public static DataClassification SensitiveData { get; } = new(TaxonomyName, nameof(SensitiveData));



        public static DataClassification PiiData { get; } = new(TaxonomyName, nameof(PiiData));
    }

    public class SensitiveDataArrtibute : DataClassificationAttribute
    {
        public SensitiveDataArrtibute() : base(DataTaxonomy.SensitiveData)
        {
        }
    }

    public class PiiDataArrtibute : DataClassificationAttribute
    {
        public PiiDataArrtibute() : base(DataTaxonomy.PiiData)
        {
        }
    }
}
