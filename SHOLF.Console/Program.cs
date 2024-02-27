using SHOLF.Library;
using System.Security.Cryptography;

int bufferSize = 2048;
int segmentCount = 3;

using (var stream = new FileStream(@$"C:\l.bin", FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, true))
{
    var alg = SHA256.Create();
    IHasher h = new Hasher();

    Console.WriteLine($@"File name: {stream.Name}.
File size: {stream.Length} byte.
Segment count: {segmentCount}.
Buffer size: {bufferSize} byte.
");

    var strs = await h.GetHashAsync(stream, alg, segmentCount, bufferSize);

    int i = 1;
    Console.WriteLine(alg.ToString() + ":");
    foreach (var str in strs)
    {
        Console.WriteLine(i + ") " + str);
        i++;
    }
    Console.WriteLine();
}