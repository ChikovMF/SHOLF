using SHOLF.Manager.UserInterface;
using SHOLF.Library;
using System.Security.Cryptography;

namespace SHOLF.Manager.Command
{
    public class HashCommand : BaseCommand, IParameterisedCommand
    {
        public HashCommand(IUserInterface ui) : base(ui) { }

        public string FilePath { get; private set; } = null!;
        public int SegmentCount { get; private set; }

        public bool GetParameters()
        {
            if (string.IsNullOrWhiteSpace(FilePath))
                FilePath = GetParameter("file path");

            if (SegmentCount == 0)
            {
                int segmentCount;
                int.TryParse(GetParameter("segment count"), out segmentCount);
                SegmentCount = segmentCount;
            }

            return (!string.IsNullOrWhiteSpace(FilePath)) && (SegmentCount > 0);
        }

        protected override bool InternalCommand()
        {
            if (!File.Exists(FilePath))
            {
                Interface.WriteWarning($"File { FilePath } does not exist!");
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

                var strs = h.GetHashAsync(stream, SHA256.Create(), SegmentCount, bufferSize).Result;

                int i = 1;
                Interface.WriteMessage("SHA256 Hashes:");
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