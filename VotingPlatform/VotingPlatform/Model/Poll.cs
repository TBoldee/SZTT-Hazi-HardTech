using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VotingPlatform.Model
{
	public class Poll : ObservableObject
	{
		public int Id  { get; set; }
		public int CreatorId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime ClosedAt { get; set; }
		public PollStatus Status { get; set; } = PollStatus.OPEN;
		public ObservableCollection<VoteOption> VoteOptions { get; set; }
		[JsonIgnore]
		public List<Vote> Votes { get; set; } = new();

		public Poll(int pollId,int userId, string title, string description, DateTime closedAt, ObservableCollection<VoteOption> voteOptions)
		{
			Id = pollId;
			CreatorId = userId;
			Title = title;
			Description = description;
			CreatedAt = DateTime.Now;
			ClosedAt = closedAt;
			VoteOptions = voteOptions;
		}

		public Poll() {}
		
		public void AddVote(Vote vote)
		{
			Votes.Add(vote);
		}

		public void ClosePoll()
		{
			ClosedAt = DateTime.Now;
			Status = PollStatus.CLOSED;
		}
	}
}
