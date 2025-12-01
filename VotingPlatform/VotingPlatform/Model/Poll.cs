using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingPlatform.Model
{
	public class Poll : ObservableObject
	{
		private static int _id = 0;
		public int Id  { get; set; }
		public int CreatorId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime ClosedAt { get; set; }
		public PollStatus Status { get; set; } = PollStatus.OPEN;
		public List<VoteOption> VoteOptions { get; set; }
		public List<Vote> Votes { get; set; } = new();

		public Poll(int userId, string title, string description, DateTime closedAt, List<VoteOption> voteOptions)
		{
			Id = _id;
			_id++;
			Title = title;
			Description = description;
			CreatedAt = DateTime.Now;
			ClosedAt = closedAt;
			VoteOptions = voteOptions;
		}
		
		public void AddVote(Vote vote)
		{
			Votes.Add(vote);
		}
	}
}
