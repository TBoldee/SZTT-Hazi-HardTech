using System.Collections.ObjectModel;
using VotingPlatform.Model;

namespace VotingPlatform.ViewModel;

public class VotingPlatformViewModel : ObservableObject
{
    public PollCreationViewModel PollCreationViewModel { get; set; }
    public AuthenticationViewModel AuthenticationViewModel { get; set; }
    public PollViewModelList OpenPollList { get; set; }
    public PollViewModelList ClosedPollList { get; set; }
    public PollViewModelList UserPollList { get; set; }
    public ObservableCollection<UserViewModel> UserViewModelList { get; set; }
    public int NextPollId => Model.NextPollId;
    public int NextVoteId => Model.NextVoteId;
    public int NextUserId => Model.NextUserId;
    public Model.VotingPlatform Model { get; set; }
    private UserViewModel _currentUser;

    public UserViewModel CurrentUser
    {
        get => _currentUser; 
        set
        {
            _currentUser = value;
            if (value != null) RefreshPolls();
        }
    }

    public VotingCommand VotingCommand { get; set; }
    public BanCommand BanCommand { get; set; }
    public Command ClosePollCommand { get; set; }

    public VotingPlatformViewModel(Model.VotingPlatform vp)
    {
        Model = vp;
        CurrentUser = null;
        PollCreationViewModel = new PollCreationViewModel(this);
        AuthenticationViewModel = new AuthenticationViewModel(this);
        OpenPollList = new PollViewModelList();
        ClosedPollList = new PollViewModelList();
        UserPollList = new PollViewModelList();
        UserViewModelList = new ObservableCollection<UserViewModel>();
        RefreshPolls();
        RefreshHighlights();
        RefreshUserViewModelList();
        VotingCommand = new VotingCommand(this);
        BanCommand = new BanCommand(this);
        ClosePollCommand = new Command<PollViewModel>(param => ClosePoll(param));
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
        foreach (var pollVm in UserPollList)
        {
            foreach (var option in pollVm.Options)
            {
                option.RefreshHighlight();
            }
        }
        foreach (var pollVm in ClosedPollList)
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
        UserPollList.Clear();

        foreach (var poll in Model.PollList.Where(poll => poll.Status == PollStatus.OPEN))
        {
            OpenPollList.Add(new PollViewModel(poll, this));
            Notify(nameof(OpenPollList));
        }

        foreach (var poll in Model.PollList.Where(poll => poll.Status == PollStatus.CLOSED))
        {
            ClosedPollList.Add(new PollViewModel(poll, this));
            Notify(nameof(ClosedPollList));
        }

        if (CurrentUser is null) return;
        foreach (var poll in Model.PollList.Where(poll => poll.CreatorId == CurrentUser.Id))
        {
            UserPollList.Add(new PollViewModel(poll, this));
            Notify(nameof(UserPollList));
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
            CurrentUser = UserViewModelList.First(u => u.Name == username);
            return true;
        }
        return false;
    }

    public bool TryRegister(string username, string password)
    {
        if (Model.Register(username, password))
        {
            RefreshUserViewModelList();
            return true;
        }
        return false;
    }

    public bool CheckIfUserExists(string username)
    {
        return Model.CheckIfUserExists(username);
    }

    public void RefreshUserViewModelList()
    {
        UserViewModelList.Clear();
        foreach (var user in Model.UserList)
        {
            var userVm = new UserViewModel(user, this);
            if (Model.BanDictionary.ContainsKey(user.Id)) userVm.Banned = true;
            UserViewModelList.Add(userVm);
        }
    }

    public void BanUser(int userId) => Model.BanUser(userId);
    public void UnbanUser(int userId) => Model.UnbanUser(userId);

    public void ClosePoll(PollViewModel poll)
    {
        Model.ClosePoll(poll.Model);
        RefreshPolls();
    }
}