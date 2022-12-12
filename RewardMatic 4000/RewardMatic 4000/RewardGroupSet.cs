using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RewardMatic_4000
{
    public class RewardGroupSet
    {
        public RewardGroup[] Groups { get; set; }

        public RewardGroupSet()
        {
        }

        public static async Task<RewardGroupSet> MakeFromFileName(string filename)
        {
            await using FileStream inputFile = File.Open(filename, FileMode.Open);
            return await JsonSerializer.DeserializeAsync<RewardGroupSet>(inputFile);
        }
    }
}