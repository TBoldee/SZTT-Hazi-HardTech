using VotingPlatform.Model;

namespace VotingPlatform.ViewModel;

public class VotingPlatformViewModel : ObservableObject
{
    public PollCreationViewModel PollCreationViewModel { get; set; }
    public PollViewModelList PollViewModelList { get; set; }
    public Model.VotingPlatform Model { get; set; }
    public UserViewModel CurrentUser { get; set; }

    public VotingPlatformViewModel(Model.VotingPlatform vp)
    {
        PollCreationViewModel = new PollCreationViewModel(this);
        Model = vp;
        PollViewModelList = new PollViewModelList(Model.PollList);
        CurrentUser = new UserViewModel(new User("Jani"));
    }
}