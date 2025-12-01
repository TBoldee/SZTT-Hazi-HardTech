namespace VotingPlatform.View;

public partial class LogInView : ContentPage
{
	public LogInView()
	{
		InitializeComponent();
	}

	private async void LogInButtonClicked(object? sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//"+nameof(AllPollsView));
	}
}