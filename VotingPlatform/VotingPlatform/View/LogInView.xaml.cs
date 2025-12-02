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
		InitializeComponent();
	}

	public async void LoggedInSuccessfully()
	{
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
}