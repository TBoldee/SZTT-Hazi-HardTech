using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingPlatform.Model
{
	public class User : ObservableObject
	{
		public int Id { get;  set; }
		public string Name { get; set; }

		public User(int userId,string name)
		{
			Id = userId;
			Name = name;
		}

		public User()
		{
			
		}
	}
}
