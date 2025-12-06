using System.Collections.ObjectModel;
using VotingPlatform.Model;
using VotingPlatform.ViewModel;
namespace DataGenerator;

class Program
{
    static void Main(string[] args)
    {
        var vp = new VotePlatform();
        AddUsers(1000,vp);
        AddPolls(1000,vp);
        AddVotes(1000,vp);
        DataSerializer.SerializeVotingPlatform(vp);
    }

    static void AddUsers(int count, VotePlatform vp)
    {
        for (int i = 1; i <= count; i++)
        {
            var user = new User(vp.NextUserId,$"User{i}");
            vp.UserList.Add(user);
        }
    }

    static void AddPolls(int count, VotePlatform vp)
    {
        for (int i = 1; i <= count; i++)
        {
            ObservableCollection<VoteOption> options = new();
            options.Add(new VoteOption($"opt 1"));
            options.Add(new VoteOption($"opt 2"));
            var poll = new Poll(vp.NextPollId,i,$"Poll{i}",$"Description{i}",DateTime.Now.AddDays(i),options);
            vp.PollList.Add(poll);
        }
    }

    static void AddVotes(int count, VotePlatform vp)
    {
        for (int p = 1; p <= count; p++)
        {
            for (int u = 1; u <= count; u++)
            {
                var vote = new Vote(vp.NextVoteId,u,p,p*2-1-(u%2));
                vp.VoteList.Add(vote);
            }
        }
    }
}