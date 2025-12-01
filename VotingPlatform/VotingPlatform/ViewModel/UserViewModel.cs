using VotingPlatform.Model;

namespace VotingPlatform.ViewModel;

public class UserViewModel
{
    public User Model {get; set;}
    public int Id => Model.Id;

    public UserViewModel(User user)
    {
        Model = user;
    }
}