using VotingPlatform.Model;

namespace VotingPlatform.ViewModel;

public class VoteOptionViewModel : ObservableObject
{
    public VoteOption Model {get; set;}
    public PollViewModel Poll {get; set;}
    public int Id => Model.Id;
    public string Text
    {
        get => Model.Text;
        set
        {
            Model.Text = value;
            Notify();
        }
    }

    public int VoteCount => Poll.Model.Votes.Count(v => v.Option == Id);

    public bool IsHighlighted
    {
        get
        {
            if (Poll.Vpvm == null) return false;
            return Poll.Vpvm.CurrentUser.HasVotedOnOption(this);
        }
    }

    public void RefreshHighlight()
    {
        Notify(nameof(IsHighlighted));
    }
    
    public VoteOptionViewModel(VoteOption model, PollViewModel poll)
    {
        Model = model;
        Poll = poll;
    }

    public void RefreshVoteCount()
    {
        Notify(nameof(VoteCount));
    }
}