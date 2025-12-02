using System.Collections.ObjectModel;
using VotingPlatform.Model;

namespace VotingPlatform.ViewModel;

public class PollViewModel : ObservableObject
{
    public Poll Model { get; set; }
    public int Id => Model.Id;
    public int CreatorId => Model.CreatorId;
    public string Title => Model.Title;
    public string Description => Model.Description;
    public DateTime CreatedAt => Model.CreatedAt;
    public DateTime ClosedAt => Model.ClosedAt;
    public PollStatus Status  => Model.Status;
    public ObservableCollection<VoteOptionViewModel> Options { get; set; } = new();
    public List<Vote> Votes { get; set; } = new();
    public VotingPlatformViewModel Vpvm { get; set; }

    public PollViewModel(Poll poll, VotingPlatformViewModel vpvm)
    {
        Model = poll;
        Vpvm = vpvm;
        foreach (var option in poll.VoteOptions)
        {
            var vovm = new VoteOptionViewModel(option, this);
            Options.Add(vovm);
        }
    }

    public void AddVote(Vote vote)
    {
        Model.AddVote(vote);
        Notify();
    }
}