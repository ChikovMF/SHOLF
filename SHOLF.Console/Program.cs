using SHOLF.Library;
using System.Security.Cryptography;

using (var stream = new FileStream(@$"C:\l2.bin", FileMode.Open, FileAccess.Read))
{
    var alg = SHA256.Create();
    var h = new Hasher();
    
    Console.WriteLine("File name: " + stream.Name + ". File size: " + stream.Length + " byte.");

    foreach(HashingAlgorithm algorithm in Enum.GetValues(typeof(HashingAlgorithm)))
    {
        var strs = h.GetHash(stream, algorithm, 3);

        int i = 1;
        Console.WriteLine(algorithm.ToString() + ":");
        foreach (var str in strs)
        {
            Console.WriteLine(i + ") " + str);
            i++;
        }
        Console.WriteLine();
    }
}