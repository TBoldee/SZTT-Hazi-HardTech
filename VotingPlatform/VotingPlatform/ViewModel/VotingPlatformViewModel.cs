using VotingPlatform.Model;

namespace VotingPlatform.ViewModel;

public class VotingPlatformViewModel : ObservableObject
{
    public PollCreationViewModel PollCreationViewModel { get; set; }
    public AuthenticationViewModel AuthenticationViewModel { get; set; }
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
        CurrentUser = null;
        PollCreationViewModel = new PollCreationViewModel(this);
        AuthenticationViewModel = new AuthenticationViewModel(this);
        OpenPollList = new PollViewModelList();
        ClosedPollList = new PollViewModelList();
        RefreshPolls();
        RefreshHighlights();
        VotingCommand = new VotingCommand(this);
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

    public void RefreshPolls()
    {
        Model.RefreshPollStatuses();
        OpenPollList.Clear();
        ClosedPollList.Clear();

        foreach (var poll in Model.PollList.Where(poll => poll.Status == PollStatus.OPEN))
        {
            OpenPollList.Add(new PollViewModel(poll, this));
        }

        foreach (var poll in Model.PollList.Where(poll => poll.Status == PollStatus.CLOSED))
        {
            ClosedPollList.Add(new PollViewModel(poll, this));
        }
    }

    public bool FirstRun()
    {
        if (Model.UserDictionary.Count == 0) return true;
        return false;
    }

    public bool TryLogIn(string username, string password)
    {
        if (Model.LogIn(username, password))
        {
            CurrentUser = new UserViewModel(Model.GetUser(username), this);
            return true;
        }
        return false;
    }

    public bool TryRegister(string username, string password)
    {
        if (Model.Register(username, password))
        {
            return true;
        }
        return false;
    }

    public bool CheckIfUserExists(string username)
    {
        return Model.CheckIfUserExists(username);
    }
}