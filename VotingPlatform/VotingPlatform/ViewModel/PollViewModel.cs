using VotingPlatform.Model;

namespace VotingPlatform.ViewModel;

public class PollViewModel : ObservableObject
{
    public Poll PollModel { get; set; }

    public PollViewModel(Poll poll)
    {
        PollModel = poll;
    }
}