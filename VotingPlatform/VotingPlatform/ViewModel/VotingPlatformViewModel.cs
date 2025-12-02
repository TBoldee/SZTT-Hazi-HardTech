using VotingPlatform.Model;

namespace VotingPlatform.ViewModel;

public class VotingPlatformViewModel : ObservableObject
{
    public PollCreationViewModel PollCreationViewModel { get; set; }
    public PollViewModelList PollViewModelList { get; set; }
    public int NextPollId => Model.NextPollId;
    public int NextVoteId => Model.NextVoteId;
    public Model.VotingPlatform Model { get; set; }
    public UserViewModel CurrentUser { get; set; }
    public VotingCommand VotingCommand { get; set; }

    public VotingPlatformViewModel(Model.VotingPlatform vp)
    {
        Model = vp;
        CurrentUser = new UserViewModel(new User("Jani"), this);
        PollCreationViewModel = new PollCreationViewModel(this);
        PollViewModelList = new PollViewModelList(Model.PollList, this);
        VotingCommand = new VotingCommand(this);
        RefreshHighlights();
    }

    public void RefreshHighlights()
    {
        foreach (var pollVm in PollViewModelList)
        {
            foreach (var option in pollVm.Options)
            {
                option.RefreshHighlight();
            }
        }
    }

    
}