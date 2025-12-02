using System.Windows.Input;
using VotingPlatform.Model;

namespace VotingPlatform.ViewModel;
public class VotingCommand :ICommand
{
    public VotingPlatformViewModel Vpvm { get; set; }
    public VotingCommand(VotingPlatformViewModel vpvm)
    {
        Vpvm = vpvm;
    }
    
    public bool CanExecute(object? parameter)
    {
        if (parameter == null) return false;
        var option = parameter as VoteOptionViewModel;
        return Vpvm.CurrentUser.CanVoteOnPoll(option);
    }

    public void Execute(object? parameter)
    {
        var option = parameter as VoteOptionViewModel;
        if (CanExecute(option))
        {
            var newVote = new Vote(Vpvm.NextVoteId, Vpvm.CurrentUser.Id, option.Poll.Id, option.Id);
            Vpvm.Model.VoteList.Add(newVote);
            Vpvm.Model.PollList.First(p => p.Id == option.Poll.Id).AddVote(newVote);
            Vpvm.RefreshHighlights();
            DataSerializer.SerializeVotingPlatform(Vpvm.Model);
        }
    }

    public event EventHandler? CanExecuteChanged;
}