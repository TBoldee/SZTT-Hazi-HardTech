using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingPlatform.Model;

namespace VotingPlatform.Tests
{
	public class ModelTests
	{
		[Fact]
		public void RegisterTest()
		{
			var vp = new VotePlatform();
			Assert.Empty(vp.UserList);
			vp.Register("TestUser", "TestPw");
			Assert.Single(vp.UserList);
			Assert.Equal("TestUser", vp.UserList[0].Name);
			Assert.Equal(1, vp.UserList[0].Id);
			vp.Register("TestUser", "TestPw2");
			Assert.Single(vp.UserList);
			Assert.Equal(1, vp.UserList[0].Id);
		}
		[Fact]
		public void LoginTest()
		{
			var vp = new VotePlatform();
			vp.Register("TestUser", "TestPw");
			
			Assert.True(vp.LogIn("TestUser", "TestPw"));
			
			Assert.False(vp.LogIn("TestUser", "WrongPw"));
			Assert.False(vp.LogIn("WrongUser", "TestPw"));
			Assert.False(vp.LogIn("WrongUser", "WrongPw"));
		}

		[Fact]
		public void RefreshPollStatusesTest()
		{
			var vp = new VotePlatform();
			vp.Register("TestUser", "TestPw");
			var user = vp.GetUser("TestUser");
			var poll = new Poll(vp.NextPollId, user.Id, "TestPoll", "TestDesc", DateTime.Now.AddDays(-1), new ObservableCollection<VoteOption>());
			var poll2 = new Poll(vp.NextPollId, user.Id, "TestPoll2", "TestDesc2", DateTime.Now.AddDays(1), new ObservableCollection<VoteOption>());
			vp.PollList.Add(poll);
			vp.PollList.Add(poll2);
			vp.RefreshPollStatuses();
			Assert.Equal(PollStatus.CLOSED, poll.Status);
			Assert.NotEqual(PollStatus.CLOSED, poll2.Status);
		}

		[Fact]
		public void GetUserTest()
		{
			var vp = new VotePlatform();
			vp.Register("TestUser", "TestPw");
			var user = vp.GetUser("TestUser");
			Assert.Equal(user,vp.UserList[0]);
			var user2 = vp.GetUser("TestUser2");
			Assert.Null(user2);
		}

		[Fact]
		public void CheckIfUserExistsTest()
		{
			var vp = new VotePlatform();
			vp.Register("TestUser", "TestPw");
			Assert.True(vp.CheckIfUserExists("TestUser"));
			Assert.False(vp.CheckIfUserExists("TestUser2"));
		}
		[Fact]
		public void AddVoteTest()
		{
			var vp = new VotePlatform();
			vp.Register("TestUser", "TestPw");
			var user = vp.GetUser("TestUser");
			var poll = new Poll(vp.NextPollId, user.Id, "TestPoll", "TestDesc", DateTime.Now.AddDays(1), new ObservableCollection<VoteOption>());
			var vote = new Vote(vp.NextVoteId, user.Id, poll.Id, 1);
			poll.AddVote(vote);
			Assert.Contains(vote, poll.Votes);
		}
		[Fact]
		public void BanUserTest()
		{
			var vp = new VotePlatform();
			vp.Register("TestUser", "TestPw");
			var user = vp.GetUser("TestUser");
			Assert.False(vp.BanDictionary.ContainsKey(user.Id));
			vp.BanUser(user.Id);
			Assert.True(vp.BanDictionary.ContainsKey(user.Id));
		}
		[Fact]
		public void UnbanUserTest()
		{
			var vp = new VotePlatform();
			vp.Register("TestUser", "TestPw");
			var user = vp.GetUser("TestUser");
			Assert.False(vp.BanDictionary.ContainsKey(user.Id));
			vp.BanUser(user.Id);
			Assert.True(vp.BanDictionary.ContainsKey(user.Id));
			vp.UnbanUser(user.Id);
			Assert.False(vp.BanDictionary.ContainsKey(user.Id));
		}
		[Fact]
		public void CheckBansTest()
		{
			var vp = new VotePlatform();
			vp.Register("TestUser", "TestPw");
			var user = vp.GetUser("TestUser");
			Assert.False(vp.BanDictionary.ContainsKey(user.Id));
			
			vp.BanUser(user.Id);
			Assert.True(vp.BanDictionary.ContainsKey(user.Id));
			
			vp.CheckBans();
			Assert.True(vp.BanDictionary.ContainsKey(user.Id));

			vp.BanDictionary[user.Id] = DateTime.Now.AddDays(-1);
			vp.CheckBans();
			Assert.False(vp.BanDictionary.ContainsKey(user.Id));
		}

		[Fact]
		public void DeletePollTest()
		{
			var vp = new VotePlatform();
			vp.Register("TestUser", "TestPw");
			var user = vp.GetUser("TestUser");
			var poll = new Poll(vp.NextPollId, user.Id, "TestPoll", "TestDesc", DateTime.Now.AddDays(1), new ObservableCollection<VoteOption>());
			var vote = new Vote(vp.NextVoteId, user.Id, poll.Id, 1);
			var vote2 = new Vote(vp.NextVoteId, user.Id, poll.Id, 2);
			vp.VoteList.Add(vote);
			vp.VoteList.Add(vote2);
			vp.PollList.Add(poll);
			
			Assert.Contains(vote, vp.VoteList);
			Assert.Contains(vote2, vp.VoteList);
			Assert.Contains(poll, vp.PollList);
			
			vp.DeletePoll(poll);
			Assert.DoesNotContain(vote, vp.VoteList);
			Assert.DoesNotContain(vote2, vp.VoteList);
			Assert.DoesNotContain(poll, vp.PollList);
		}
	}
}
