using System.Windows.Input;

namespace VotingPlatform.ViewModel;

public class BanCommand : ICommand
{
    public VotingPlatformViewModel Vpvm { get; set; }

    public BanCommand(VotingPlatformViewModel vpvm)
    {
        Vpvm = vpvm;
    }
    public bool CanExecute(object? parameter)
    {
        if (parameter is null) return false;
        var user = parameter as UserViewModel;
        return user.Id != 1;
    }

    public void Execute(object? parameter)
    {
        if (CanExecute(parameter))
        {
            var user = parameter as UserViewModel;
            if (user.Banned)
            {
                Vpvm.UnbanUser(user.Id);
                user.Banned = false;
            }
            else
            {
                Vpvm.BanUser(user.Id);
                user.Banned = true;
            }
        }
    }

    public event EventHandler? CanExecuteChanged;
}