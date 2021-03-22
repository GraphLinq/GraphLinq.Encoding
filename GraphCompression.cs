using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using NodeBlock.Engine.Encoding;

namespace NodeBlock.Engine.Encoding
{
    public static class GraphCompression
    {
        public static string DecompressGraphData(string input)
        {
            byte[] compressed = Convert.FromBase64String(input);
            byte[] decompressed = GzipCompression.Decompress(compressed);
            return System.Text.Encoding.UTF8.GetString(decompressed);
        }

        public static string CompressGraphData(string input)
        {
            byte[] encoded = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] compressed = GzipCompression.Compress(encoded);
            return Convert.ToBase64String(compressed);
        }

        public static string GetUniqueGraphHash(int identifier, string compressedData)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(string.Format("{0},{1}", Convert.ToString(identifier), compressedData)));

                // Convert byte array to a string 
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
