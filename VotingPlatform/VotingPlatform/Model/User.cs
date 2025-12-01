using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingPlatform.Model
{
	public class User : ObservableObject
	{
		private static int _id = 0;
		public int Id { get;  set; }
		public string Name { get; set; }

		public User(string name)
		{
			Id = _id;
			_id++;
			Name = name;
		}
	}
}
