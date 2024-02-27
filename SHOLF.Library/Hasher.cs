using System.Security.Cryptography;
using System.Text;

namespace SHOLF.Library;

public class Hasher : IHasher
{
    public async Task<ICollection<string>> GetHashAsync(Stream stream, HashAlgorithm hashAlgorithm, int segmentCount, int bufferSize)
    {
        HashAlgorithm algorithm = hashAlgorithm;
        long c = (long)Math.Ceiling((decimal)stream.Length / segmentCount);

        string[] result = new string[segmentCount];
        byte[] buffer = new byte[bufferSize];

        for (int i = 0; i < segmentCount; i++)
        {
            stream.Position = i * c;
            long bytesToHash = c;

            while (bytesToHash > 0)
            {
                var bytesRead = await stream.ReadAsync(buffer, 0, (int)Math.Min(bytesToHash, buffer.Length));

                algorithm.TransformBlock(buffer, 0, bytesRead, null, 0);

                bytesToHash -= bytesRead;

                if (bytesRead == 0)
                    bytesToHash = 0;
            }

            algorithm.TransformFinalBlock(buffer, 0, 0);

            byte[] hash = algorithm.Hash ?? new byte[0];

            result[i] = ByteToString(hash);
        }

        return result;
    }

    private string ByteToString(byte[] array, bool format = false)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < array.Length; i++)
        {
            sb.Append($"{array[i]:X2}");

            if ( format && (i % 4) == 3) sb.Append(" ");
        }

        return sb.ToString();
    }
}