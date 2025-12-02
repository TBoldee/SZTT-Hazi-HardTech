using System.Collections.ObjectModel;
using VotingPlatform.Model;

namespace VotingPlatform.ViewModel;

public class PollViewModelList : ObservableCollection<PollViewModel>
{
    public PollViewModelList(List<Poll> polls, VotingPlatformViewModel vpvm)
    {
        foreach (var poll in polls)
        {
            Add(new PollViewModel(poll, vpvm));
        }
    }
}