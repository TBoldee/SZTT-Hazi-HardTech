using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace VotingPlatform.Model
{
	public class DataSerializer
	{
		private static JsonSerializerOptions _options =  new() {WriteIndented = true};

		public static void SerializeVotingPlatform(VotingPlatform vp)
		{
			var serialized = JsonSerializer.Serialize(vp, _options);
			File.WriteAllText("VotingPlatform.json", serialized);
		}

		public static VotingPlatform DeserializeVotingPlatform()
		{
			var deserialized = File.ReadAllText("VotingPlatform.json");
			return JsonSerializer.Deserialize<VotingPlatform>(deserialized, _options);
		}

		public static void SerializeUserDict(Dictionary<int, string> userDict)
		{
			var sb = new StringBuilder();
			foreach (var kvp in userDict)
			{
				sb.AppendLine($"{kvp.Key}:{kvp.Value}");
			}
			File.WriteAllText("UserDict.txt", sb.ToString());
		}

		public static Dictionary<int, string> DeserializeUserDict()
		{
			var lines = File.ReadLines("UserDict.txt");
			return lines.Select(line => line.Split(':')).ToDictionary(split => int.Parse(split[0]), split => split[1]);
		}
	}
}
