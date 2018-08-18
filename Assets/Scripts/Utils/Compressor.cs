using Ionic.Zip;

namespace Underlunchers.Utils
{
    public static class Compressor 
    {
        public static void CompressDirectory(string from, string to)
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(from);
                zip.Save(to);
            }
        }

        public static void DecompressToDirectory(string from)
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.ExtractAll(from, ExtractExistingFileAction.OverwriteSilently);
            }
        }
    }
}