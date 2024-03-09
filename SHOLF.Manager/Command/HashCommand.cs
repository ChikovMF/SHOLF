using SHOLF.Manager.UserInterface;
using SHOLF.Library;
using System.Security.Cryptography;
using System.Runtime.Intrinsics.Arm;
using System.Buffers.Text;

namespace SHOLF.Manager.Command
{
    public class HashCommand : BaseCommand, IParameterisedCommand
    {
        public HashCommand(IUserInterface ui) : base(ui) { }

        public string FilePath { get; private set; } = null!;
        public int SegmentCount { get; private set; }
        public HashAlgorithm Algorithm { get; private set; } = default!;

        public bool GetParameters()
        {
            if (string.IsNullOrWhiteSpace(FilePath))
            {
                FilePath = GetParameter("file path");

                if (!File.Exists(FilePath))
                {
                    Interface.WriteWarning("File does not exist");
                    FilePath = string.Empty;
                    return false;
                }

                if (string.IsNullOrWhiteSpace(FilePath))
                {
                    Interface.WriteWarning("File path is empty");
                    FilePath = string.Empty;
                    return false;
                }
            }

            if (SegmentCount == 0)
            {
                int segmentCount;
                int.TryParse(GetParameter("segment count"), out segmentCount);
                SegmentCount = segmentCount;

                if (SegmentCount <= 0)
                {
                    Interface.WriteWarning("Segment count is invalid");
                    SegmentCount = 0;
                    return false;
                }
            }

            if (Algorithm == default(HashAlgorithm))
            {
                Interface.WriteMessage("Please select a hash algorithm:");
                Interface.WriteMessage("\t1. MD5");
                Interface.WriteMessage("\t2. SHA1");
                Interface.WriteMessage("\t3. SHA256");
                Interface.WriteMessage("\t4. SHA512");

                string input = Interface.ReadValue("Enter alghorithm number: ");

                switch (input)
                {
                    case "1":
                        Algorithm = MD5.Create();
                        break;
                    case "2":
                        Algorithm = SHA1.Create();
                        break;
                    case "3":
                        Algorithm = SHA256.Create();
                        break;
                    case "4":
                        Algorithm = SHA512.Create();
                        break;
                    default:
                        Interface.WriteWarning("Invalid alghorithm number");
                        Algorithm = default(HashAlgorithm)!;
                        return false;
                }
            }

            return true;
        }

        protected override bool InternalCommand()
        {
            if (!File.Exists(FilePath))
            {
                Interface.WriteWarning("File does not exist!");
                return false;
            }

            const int bufferSize = 2048;

            using (var stream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, true))
            {
                IHasher h = new Hasher();

                Interface.WriteMessage($"File name: {stream.Name}." +
                                        $"\nFile size: {stream.Length} byte." +
                                        $"\nSegment count: {SegmentCount}." +
                                        $"\nBuffer size: {bufferSize} byte."
                                        );

                var strs = h.GetHashAsync(stream, Algorithm, SegmentCount, bufferSize).Result;

                int i = 1;
                Interface.WriteMessage($"{Algorithm} hash result:");
                foreach (var str in strs)
                {
                    Interface.WriteMessage(i + ". " + str);
                    i++;
                }
                Interface.WriteMessage("\n");

                return true;
            }
        }
    }
}