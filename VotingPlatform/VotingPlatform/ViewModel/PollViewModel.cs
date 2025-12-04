using System.Collections.ObjectModel;
using VotingPlatform.Model;

namespace VotingPlatform.ViewModel;

public class PollViewModel : ObservableObject
{
    public Poll Model { get; set; }
    public int Id => Model.Id;
    public int CreatorId => Model.CreatorId;

    public string Title
    {
        get => Model.Title;
        set
        {
            Model.Title = value;
            Notify();
        }
    }
    public string Description
    {
        get => Model.Description;
        set
        {
            Model.Description = value;
            Notify();
        }
    }
    public DateTime CreatedAt => Model.CreatedAt;
    public DateTime ClosedAt => Model.ClosedAt;
    public PollStatus Status  => Model.Status;
    private ObservableCollection<VoteOptionViewModel> _options = new();

    public ObservableCollection<VoteOptionViewModel> Options
    {
        get => _options;
        set
        {
            if (value.Count == 0) return;
            _options = value;
            Model.VoteOptions.Clear();
            foreach (var option in _options) Model.VoteOptions.Add(option.Model);
        }
    }
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
    public void NotifyAllPropertiesChanged()
    {
        Notify(nameof(Title));
        Notify(nameof(Description));
        Notify(nameof(Options));
    }
}