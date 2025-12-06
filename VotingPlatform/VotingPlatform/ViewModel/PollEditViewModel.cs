using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using VotingPlatform.Model;

namespace VotingPlatform.ViewModel;

public class PollEditViewModel : ObservableObject
{
    public VotingPlatformViewModel Vpvm { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public ObservableCollection<VoteOptionViewModel> Options { get; set; } = new();
    private List<VoteOptionViewModel> _optionsWithVotes { get; set; } = new();
    public Command AddOptionCommand { get; set; }
    public Command<VoteOptionViewModel> RemoveOptionCommand {get; set; }
    public Command FinalizePollCommand { get; set; }
    private PollViewModel _originalPoll;
    public event Action? EditFailed;
    public event Action? EditSuccessful;
    public PollEditViewModel(VotingPlatformViewModel vpvm)
    {
        Vpvm = vpvm;
        AddOptionCommand = new Command(AddOption);
        RemoveOptionCommand = new Command<VoteOptionViewModel>(param => RemoveOption(param));
        FinalizePollCommand = new Command(()=>Save());
    }

    public void Load(PollViewModel poll)
    {
        _originalPoll = poll;
        Title = poll.Title;
        Description = poll.Description;
        Options = new ObservableCollection<VoteOptionViewModel>(poll.Options);
        _optionsWithVotes = new List<VoteOptionViewModel>();
        RemoveOptionsWithVotes();
        NotifyAllPropertiesChanged();
    }

    private void Save()
    {
        if (ValidatePollDetails())
        {
            _originalPoll.Title = Title;
            _originalPoll.Description = Description;
            foreach (var option in _optionsWithVotes) Options.Add(option); //visszarakni a kiszedett option-öket
            _originalPoll.Options = Options;
            DataSerializer.SerializeVotingPlatform(Vpvm.Model);
            Vpvm.RefreshPolls();
            EditSuccessful?.Invoke();
        }
    }
    
    private void NotifyAllPropertiesChanged()
    {
        Notify(nameof(Title));
        Notify(nameof(Description));
        Notify(nameof(Options));
    }
    
    private void AddOption()
    {
        Options.Add(new VoteOptionViewModel(new VoteOption(""), _originalPoll));
        Notify(nameof(Options));
    }
    
    private void RemoveOption(VoteOptionViewModel option)
    {
        Options.Remove(option);
        Notify(nameof(Options));
    }

    private void RemoveOptionsWithVotes()
    {
        _optionsWithVotes = Options.Where(o => o.Poll.Model.Votes.Any(v => v.Option == o.Id)).ToList();
        foreach (var option in _optionsWithVotes)
        {
            RemoveOption(option);
        }
    }
    private bool ValidatePollDetails()
    {
        var re = new Regex("""^\s*$""");
        bool optionsInvalid = Options.Any(opt => re.IsMatch(opt.Text));
        if (Options.Count + _optionsWithVotes.Count < 2) optionsInvalid = true;
        var allOptionTexts = Options.Concat(_optionsWithVotes).Select(o => o.Text).ToList();
        if (allOptionTexts.Count != allOptionTexts.Distinct().Count()) optionsInvalid = true;

        if (re.IsMatch(Title) ||
            re.IsMatch(Description) ||
            optionsInvalid)
        {
            EditFailed?.Invoke();
            return false;
        }
        return true;
    }
}