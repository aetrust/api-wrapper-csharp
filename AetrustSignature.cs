namespace authutils_csharp
{
    public class AetrustSignature
    {
        public AetrustSignature(string signature, long timestamp)
        {
            Signature = signature;
            Timestamp = timestamp;
        }

        public string Signature { get; private set; }
        public long Timestamp { get; private set; }
    }
}
