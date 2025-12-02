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
		InitializeComponent();
	}

	public async void LoggedInSuccessfully()
	{
		await Shell.Current.GoToAsync("//"+nameof(OpenPollsView));
	}
}