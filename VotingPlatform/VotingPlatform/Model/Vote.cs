using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingPlatform.Model
{
	public class Vote : ObservableObject
	{
		public int VoteId { get; set; }
		public int UserId { get; set; }
		public int PollId { get; set; }
		public int Option { get; set; }

		public Vote(int voteId, int userId, int pollId, int option)
		{
			VoteId = voteId;
			UserId = userId;
			PollId = pollId;
			Option = option;
		}
	}
}
