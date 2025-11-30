using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingPlatform.Model
{
	public class Poll
	{
		private static int _id = 0;
		public int Id  { get; set; }
		public int CreatorId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime ClosedAt { get; set; }
		public PollStatus Status { get; set; } = PollStatus.OPEN;

		public Poll(int userId, string title, string description, DateTime closedAt)
		{
			Id = _id;
			_id++;
			Title = title;
			Description = description;
			CreatedAt = DateTime.Now;
			ClosedAt = closedAt;
		}
	}
}
