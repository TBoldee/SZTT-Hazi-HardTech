using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VotingPlatform.Model
{
	public class VotingPlatform : ObservableObject
	{
		public List<User> UserList { get; set; } = new();
		public List<Poll> PollList { get; set; } = new();
		[JsonInclude]
		private int lastPollId = 0;
		[JsonIgnore]
		public int NextPollId => ++lastPollId;
		public List<Vote> VoteList { get; set; } = new();
		[JsonInclude]
		public int lastVoteId = 0;
		[JsonIgnore]
		public int NextVoteId => ++lastVoteId;
		[JsonIgnore]
		public Dictionary<string, string> UserDictionary { get; set; } = new(); //username,hash
		
		[OnDeserialized]
		internal void InitializeRelations()
		{
			UserDictionary = DataSerializer.DeserializeUserDict();
			ConnectPollsToVotes();
		}
		public bool LogIn(string username, string password)
		{
			foreach (var kvp in UserDictionary)
			{
				if (kvp.Key.Equals(username) && HashPassword(kvp.Value).Equals(password)) return true;
			}
			return false;
		}

		public bool Register(string username, string password)
		{
			if (UserDictionary.Keys.Any(name => name.Equals(username))) return  false;
			else
			{
				var hash = HashPassword(password);
				UserDictionary.Add(username, hash);
				DataSerializer.SerializeUserDict(UserDictionary);
				return true;
			}
		}
		
		private static string HashPassword(string password)
		{
			var byteArray = SHA256.HashData(Encoding.UTF8.GetBytes(password));
			return Convert.ToHexString(byteArray);
		}

		private void ConnectPollsToVotes()
		{
			foreach (var vote in VoteList)
			{
				foreach (var poll in PollList)
				{
					if (vote.PollId == poll.Id) poll.AddVote(vote);
					break;
				}
			}
		}
	}
}
