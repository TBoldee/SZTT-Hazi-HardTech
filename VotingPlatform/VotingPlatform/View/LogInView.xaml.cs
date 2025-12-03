using VotingPlatform.ViewModel;

namespace VotingPlatform.View;

public partial class LogInView : ContentPage
{
	public AuthenticationViewModel ViewModel {get; set;}
	public LogInView()
	{
		this.ViewModel = AppShell.VPVM.AuthenticationViewModel;
		BindingContext = this.ViewModel;
		ViewModel.LoggedIn += LoggedInSuccessfully;
		ViewModel.Registered += RegisteredSuccessfully;
		ViewModel.RegisterFailed += RegistrationFailed;
		ViewModel.BannedLogin += BannedLogin;
		InitializeComponent();
	}

	public async void LoggedInSuccessfully()
	{
		if (ViewModel.Vpvm.CurrentUser.Id != 1)
		{
			var userListTab = Shell.Current.Items[1].Items.Where(s => s.Title == "User List").FirstOrDefault();
			if (userListTab != null) Shell.Current.Items[1].Items.Remove(userListTab);
		}
		await Shell.Current.GoToAsync("//"+nameof(OpenPollsView));
	}

	public async void RegisteredSuccessfully()
	{
		await DisplayAlert("Registration Successful", "", "Ok");
	}

	public async void RegistrationFailed()
	{
		await DisplayAlert("Registration Failed", "", "Ok");
	}
	public async void BannedLogin(DateTime? time)
	{
		await DisplayAlert("You are currently banned!", $"You are banned until: {time}", "Ok");
	}
}