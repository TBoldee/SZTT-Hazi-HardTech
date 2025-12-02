using VotingPlatform.Model;

namespace VotingPlatform.ViewModel;

public class VotingPlatformViewModel : ObservableObject
{
    public PollCreationViewModel PollCreationViewModel { get; set; }
    public PollViewModelList OpenPollList { get; set; }
    public PollViewModelList ClosedPollList { get; set; }
    public int NextPollId => Model.NextPollId;
    public int NextVoteId => Model.NextVoteId;
    public int NextUserId => Model.NextUserId;
    public Model.VotingPlatform Model { get; set; }
    public UserViewModel CurrentUser { get; set; }
    public VotingCommand VotingCommand { get; set; }

    public VotingPlatformViewModel(Model.VotingPlatform vp)
    {
        Model = vp;
        CurrentUser = new UserViewModel(new User("Jani"), this);
        PollCreationViewModel = new PollCreationViewModel(this);
        OpenPollList = new PollViewModelList(Model.PollList, this);
        ClosedPollList = new PollViewModelList(Model.PollList, this);
        VotingCommand = new VotingCommand(this);
        RefreshHighlights();
    }

    public void RefreshHighlights()
    {
        foreach (var pollVm in OpenPollList)
        {
            foreach (var option in pollVm.Options)
            {
                option.RefreshHighlight();
            }
        }
    }

    
}