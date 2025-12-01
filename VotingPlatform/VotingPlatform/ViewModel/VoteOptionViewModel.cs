using VotingPlatform.Model;

namespace VotingPlatform.ViewModel;

public class VoteOptionViewModel : ObservableObject
{
    public VoteOption Model {get; set;}
    public int Id => Model.Id;
    public string Text => Model.Text;
    
    public VoteOptionViewModel(VoteOption model)
    {
        Model = model;
    }
}