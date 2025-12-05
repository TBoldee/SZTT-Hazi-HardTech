using System;
using System.Collections.Generic;
using System.Globalization;
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
			if (!File.Exists("VotingPlatform.json"))
			{
				var file = File.Create("VotingPlatform.json");
				file.Close();
			}
			var deserialized = File.ReadAllText("VotingPlatform.json");
			if (deserialized == "") return new VotingPlatform();
			var vp = JsonSerializer.Deserialize<VotingPlatform>(deserialized, _options);
			return vp;
		}

		public static void SerializeUserDict(Dictionary<string, string> userDict)
		{
			var sb = new StringBuilder();
			foreach (var kvp in userDict)
			{
				sb.AppendLine($"{kvp.Key}:{kvp.Value}");
			}
			File.WriteAllText("UserDict.txt", sb.ToString());
		}

		public static Dictionary<string, string> DeserializeUserDict()
		{
			if (!File.Exists("UserDict.txt")) File.Create("UserDict.txt");
			var lines = File.ReadLines("UserDict.txt");
			if (!lines.Any()) return new Dictionary<string, string>();
			return lines.Select(line => line.Split(':')).ToDictionary(split => split[0], split => split[1]);
		}
		public static void SerializeBanDict(Dictionary<int, DateTime> banDict)
		{
			var sb = new StringBuilder();
			foreach (var kvp in banDict)
			{
				sb.AppendLine($"{kvp.Key} | {kvp.Value:O}");
			}
			File.WriteAllText("BanDict.txt", sb.ToString());
		}

		public static Dictionary<int, DateTime> DeserializeBanDict()
		{
			if (!File.Exists("BanDict.txt")) File.Create("BanDict.txt");
			var lines = File.ReadLines("BanDict.txt");
			if (!lines.Any()) return new Dictionary<int, DateTime>();
			return lines.Select(line => line.Split(" | ")).
				ToDictionary(split => Convert.ToInt32(split[0]), split => DateTime.ParseExact(split[1],"O", null));
		}
	}
}
