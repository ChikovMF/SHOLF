using System.Security.Cryptography;
using System.Text;

namespace SHOLF.Library;

public class Hasher : IHasher
{
    public string[] GetHash(Stream stream, HashingAlgorithm hashingAlgorithm , int segmentCount)
    {
        HashAlgorithm algorithm = GetAlgoritm(hashingAlgorithm);

        long c = (long)Math.Ceiling((decimal)stream.Length / segmentCount);

        string[] result = new string[segmentCount];
        byte[] buffer = new byte[4 * 1024];

        for (int i = 0; i < segmentCount; i++)
        {
            stream.Position = i * c;
            long bytesToHash = c;

            while (bytesToHash > 0)
            {
                var bytesRead = stream.Read(buffer, 0, (int)Math.Min(bytesToHash, buffer.Length));

                algorithm.TransformBlock(buffer, 0, bytesRead, null, 0);

                bytesToHash -= bytesRead;

                if (bytesRead == 0)
                    bytesToHash = 0;
            }

            algorithm.TransformFinalBlock(buffer, 0, 0);

            byte[] hash = algorithm.Hash;

            result[i] = ByteToString(hash);
        }

        return result;
    }

    private string ByteToString(byte[] array)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < array.Length; i++)
        {
            sb.Append($"{array[i]:X2}");

            // if ((i % 4) == 3) sb.Append(" ");
        }

        return sb.ToString();
    }

    private HashAlgorithm GetAlgoritm(HashingAlgorithm algorithm)
    {
        switch (algorithm)
        {
            case HashingAlgorithm.SHA512:
                return SHA512.Create();

            case HashingAlgorithm.SHA256:
                return SHA256.Create();

            case HashingAlgorithm.SHA1:
                return SHA1.Create();

            case HashingAlgorithm.MD5:
                return MD5.Create();

            default: throw new ArgumentException("Unsupported hash algorithm value", nameof(algorithm));
        }
    }
}