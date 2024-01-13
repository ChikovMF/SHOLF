using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;

namespace SHOLF.Library;

public interface IHasher
{
    string[] GetHash(Stream stream, HashingAlgorithm hashingAlgorithm, int segmentCount);
}
