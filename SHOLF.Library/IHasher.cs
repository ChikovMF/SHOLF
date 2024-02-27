using System.Security.Cryptography;

namespace SHOLF.Library;

public interface IHasher
{
    Task<ICollection<string>> GetHashAsync(Stream stream, HashAlgorithm hashAlgorithm, int segmentCount = 1, int bufferSize = 1024);
}
