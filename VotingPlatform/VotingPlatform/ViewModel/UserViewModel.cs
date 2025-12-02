using VotingPlatform.Model;

namespace VotingPlatform.ViewModel;

public class UserViewModel
{
    public User Model {get; set;}
    public VotingPlatformViewModel Vpvm { get; set; }
    public int Id => Model.Id;

    public UserViewModel(User user, VotingPlatformViewModel vpvm)
    {
        Model = user;
        Vpvm = vpvm;
    }
    
    public bool CanVoteOnPoll(VoteOptionViewModel option)
    {
        if (option.Poll.ClosedAt < DateTime.Now) return false;
        return !Vpvm.Model.VoteList.AsParallel().Where(v => v.PollId == option.Poll.Id).Any(v => v.UserId == Id);
    }

    public bool HasVotedOnOption(VoteOptionViewModel option)
    {
        var asd = Vpvm.Model.VoteList.AsParallel().Where(v => v.Option == option.Id).Any(v => v.UserId == Id);
        return asd;
    }
}