using VotingPlatform.Model;

namespace VotingPlatform.ViewModel;

public class AuthenticationViewModel : ObservableObject
{
    public string Title {get; set;}
    public string Username {get; set;}
    public string Password {get; set;}
    public VotingPlatformViewModel Vpvm {get; set;}
    public Command ShowRegisterCommand { get; }
    public Command ShowLoginCommand { get; }
    public Command RegisterCommand { get; }
    public Command LoginCommand { get; }
    private bool _isRegistering;
    public bool IsRegistering
    {
        get => _isRegistering;
        set 
        { 
            _isRegistering = value;
            Title = (value == false) ? "Login" : "Register";
            Notify();
        }
    }
    public event Action? LoggedIn;
    public event Action? Registered;

    public AuthenticationViewModel(VotingPlatformViewModel vpvm)
    {
        Vpvm = Vpvm;
        ResetAllFields();
        ShowRegisterCommand = new Command(() => IsRegistering = true);
        ShowLoginCommand = new Command(() => IsRegistering = false);
    }

    private void ResetAllFields()
    {
        Username = "";
        Password = "";
        NotifyAllProperties();
    }

    private void NotifyAllProperties()
    {
        Notify(nameof(Title));
        Notify(nameof(Username));
        Notify(nameof(Password));
        Notify(nameof(IsRegistering));
    }
}