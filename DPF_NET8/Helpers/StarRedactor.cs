using Microsoft.Extensions.Compliance.Redaction;

namespace DPF_NET8.Helpers
{
    public class StarRedactor : Redactor
    {
        public override int GetRedactedLength(ReadOnlySpan<char> input)
        {
            return input.Length;
        }

        public override int Redact(ReadOnlySpan<char> source, Span<char> destination)
        {
            destination.Fill('*');
            return destination.Length;
        }
    }
}
