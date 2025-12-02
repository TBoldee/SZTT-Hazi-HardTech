using System.Text.RegularExpressions;
using VotingPlatform.Model;

namespace VotingPlatform.ViewModel;

public class AuthenticationViewModel : ObservableObject
{
    private string _title;
    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            Notify();
        }
    }

    public string LoginUsername {get; set;}
    public string LoginPassword {get; set;}
    public string RegisterUsername {get; set;}
    public string RegisterPassword {get; set;}
    public string RegisterPasswordConfirm {get; set;}
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
            if (value && Vpvm.FirstRun()) Title = "Register Admin";
            else if (value && !Vpvm.FirstRun()) Title = "Register User";
            else Title = "Login";
            Notify();
            Notify(nameof(IsLoggingIn));
        }
    }
    public bool IsLoggingIn => !IsRegistering;
    public event Action? LoggedIn;
    public event Action? Registered;
    public event Action? RegisterFailed;

    public AuthenticationViewModel(VotingPlatformViewModel vpvm)
    {
        Vpvm = vpvm;
        Title = "Login";
        IsRegistering = false;
        ResetAllFields();
        ShowRegisterCommand = new Command(() => IsRegistering = true);
        ShowLoginCommand = new Command(() => IsRegistering = false);
        LoginCommand = new Command(() => TryLogIn());
        RegisterCommand = new Command(() => TryRegister());
    }

    private void ResetAllFields()
    {
        LoginUsername = "Username";
        LoginPassword = "Password";
        RegisterUsername = "Username";
        RegisterPassword = "Password";
        RegisterPasswordConfirm = "Password again";
        NotifyAllProperties();
    }

    private void NotifyAllProperties()
    {
        Notify(nameof(Title));
        Notify(nameof(LoginUsername));
        Notify(nameof(LoginPassword));
        Notify(nameof(RegisterUsername));
        Notify(nameof(RegisterPassword));
        Notify(nameof(RegisterPasswordConfirm));
        Notify(nameof(IsRegistering));
    }

    private bool TryLogIn()
    {
        if (Vpvm.TryLogIn(LoginUsername, LoginPassword))
        {
            LoggedIn?.Invoke();
            return true;
        }
        return false;
    }

    private bool TryRegister()
    {
        if (!ValidateRegistrationEntries())
        {
            RegisterFailed?.Invoke();
            return false;
        }
        if (Vpvm.TryRegister(RegisterUsername, RegisterPassword))
        {
            Registered?.Invoke();
            IsRegistering = false;
            ResetAllFields();
            return true;
        }
        return false;
    }

    private bool ValidateRegistrationEntries()
    {
        var re = new Regex("""^\s*$""");
        if (re.IsMatch(RegisterUsername) || re.IsMatch(RegisterPassword) ||
            re.IsMatch(RegisterPasswordConfirm)) return false;
        if (Vpvm.CheckIfUserExists(RegisterUsername)) return false;
        if (!RegisterPassword.Equals(RegisterPasswordConfirm)) return false;
        return true;
    }
}