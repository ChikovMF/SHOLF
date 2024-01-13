using SHOLF.Library;
using System.Security.Cryptography;

using (var stream = new FileStream(@$"C:\l.bin", FileMode.Open, FileAccess.Read))
{
    var alg = SHA256.Create();
    var h = new Hasher();
    var strs = h.GetHash(stream, HashingAlgorithm.MD5, 1);

    int i = 1;
    foreach (var str in strs)
    {
        Console.WriteLine(i + ") " + str);
        i++;
    }
}