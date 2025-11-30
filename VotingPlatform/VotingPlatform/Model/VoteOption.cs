using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingPlatform.Model
{
	public class VoteOption
	{
		private static int _id = 0;
		public int Id { get; set; }
		public string Text { get; set; }
		public VoteOption(string text)
		{
			Id = _id;
			_id++;
			Text = text;
		}
	}
}
