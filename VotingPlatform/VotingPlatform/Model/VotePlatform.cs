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
	public class VotePlatform : ObservableObject
	{
		public List<User> UserList { get; set; } = new();
		public List<Poll> PollList { get; set; } = new();
		public List<Vote> VoteList { get; set; } = new();
		[JsonInclude]
		private int lastPollId = 0;
		[JsonIgnore]
		public int NextPollId => ++lastPollId;
		[JsonInclude]
		private int lastVoteId = 0;
		[JsonIgnore]
		public int NextVoteId => ++lastVoteId;
		[JsonIgnore]
		public Dictionary<string, string> UserDictionary { get; set; } = new(); //username,hash
		[JsonIgnore]
		public Dictionary<int, DateTime> BanDictionary { get; set; } = new();

		[JsonInclude]
		private int lastUserId = 0;
		[JsonIgnore]
		public int NextUserId => ++lastUserId;

		[JsonConstructor]
		public VotePlatform(List<User> userlist, List<Poll> polllist, List<Vote> votelist, int lastpollId, int lastvoteid, int lastuserid)
		{
			UserList = userlist;
			PollList = polllist;
			VoteList = votelist;
			lastPollId = lastpollId;
			lastVoteId = lastvoteid;
			lastUserId = lastuserid;
			InitializeRelations();
		}

		public VotePlatform()
		{

		}
		internal void InitializeRelations()
		{
			UserDictionary = DataSerializer.DeserializeUserDict();
			BanDictionary = DataSerializer.DeserializeBanDict();
			CheckBans();
			ConnectPollsToVotes();
		}
		public bool LogIn(string username, string password)
		{
			foreach (var kvp in UserDictionary)
			{
				if (kvp.Key.Equals(username) && HashPassword(password).Equals(kvp.Value)) return true;
			}
			return false;
		}

		public bool Register(string username, string password)
		{
			if (UserDictionary.Keys.Any(name => name.Equals(username))) return  false;
			if (username.Equals("") || password.Equals("")) return false;
			if (username.Contains('|') || username.Contains(':') || password.Contains('|') || password.Contains(':')) return false;
			var hash = HashPassword(password);
			UserDictionary.Add(username, hash);
			UserList.Add(new User(NextUserId, username));
			DataSerializer.SerializeUserDict(UserDictionary);
			DataSerializer.SerializeVotingPlatform(this);
			return true;
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
					if (vote.PollId == poll.Id)
					{
						poll.AddVote(vote);
						break;
					}
				}
			}
		}

		public void RefreshPollStatuses()
		{
			foreach (var poll in PollList)
			{
				if (poll.ClosedAt < DateTime.Now) poll.Status = PollStatus.CLOSED;
			}
		}

		public User GetUser(string username)
		{
			return UserList.Where(u => u.Name.Equals(username)).FirstOrDefault();
		}

		public bool CheckIfUserExists(string username)
		{
			return UserList.Any(u => u.Name.Equals(username));
		}

		public void BanUser(int userId)
		{
			BanDictionary.Add(userId, DateTime.Now.AddDays(1));
			DataSerializer.SerializeBanDict(BanDictionary);
		}

		public void UnbanUser(int userId)
		{
			BanDictionary.Remove(userId);
			DataSerializer.SerializeBanDict(BanDictionary);
		}

		public void ClosePoll(Poll poll)
		{
			PollList.First(p => p == poll).ClosePoll();
		}

		public void DeletePoll(Poll poll)
		{
			PollList.Remove(poll);
			var votes = VoteList.Where(v => v.PollId == poll.Id).ToList();
			foreach (var vote in votes)
			{
				VoteList.Remove(vote);
			}
			DataSerializer.SerializeVotingPlatform(this);
		}

		public void CheckBans()
		{
			foreach (var kvp in BanDictionary)
			{
				if (kvp.Value < DateTime.Now) BanDictionary.Remove(kvp.Key);
			}
		}
	}
}
