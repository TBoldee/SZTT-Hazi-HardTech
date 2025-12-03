using VotingPlatform.Model;

namespace VotingPlatform.ViewModel;

public class UserViewModel : ObservableObject
{
    public User Model {get; set;}
    public VotingPlatformViewModel Vpvm { get; set; }
    public int Id => Model.Id;
    public string Name => Model.Name;
    private bool _banned;

    public bool Banned
    {
        get => _banned;
        set
        {
            _banned = value;
            Notify();
            Notify(nameof(Unbanned));
        }
    }
    public bool Unbanned => !Banned;
    public DateTime? BannedUntil = null;

    public UserViewModel(User user, VotingPlatformViewModel vpvm)
    {
        Model = user;
        Vpvm = vpvm;
        Banned = false;
        BannedUntil = TryGetBanExpiration();
    }
    
    public bool CanVoteOnPoll(VoteOptionViewModel option)
    {
        if (Banned) return false;
        if (option.Poll.ClosedAt < DateTime.Now) return false;
        return !Vpvm.Model.VoteList.AsParallel().Where(v => v.PollId == option.Poll.Id).Any(v => v.UserId == Id);
    }

    public bool HasVotedOnOption(VoteOptionViewModel option)
    {
        return Vpvm.Model.VoteList.AsParallel().Where(v => v.Option == option.Id).Any(v => v.UserId == Id);
    }

    private DateTime? TryGetBanExpiration()
    {
        var dict = Vpvm.Model.BanDictionary;
        if (dict.ContainsKey(Model.Id)) return dict[Model.Id];
        return null;
    }
}