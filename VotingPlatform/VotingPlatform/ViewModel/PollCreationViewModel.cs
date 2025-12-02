using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using VotingPlatform.Model;

namespace VotingPlatform.ViewModel;

public class PollCreationViewModel : ObservableObject
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ClosedAt { get; set; }
    public ObservableCollection<VoteOption> Options { get; set; }
    public VotingPlatformViewModel Vpvm { get; set; }
    public Command CreatePollCommand { get; set; }
    public Command AddOptionCommand { get; set; }

    public PollCreationViewModel(VotingPlatformViewModel vp)
    {
        ResetAllFields();
        CreatePollCommand = new Command(CreatePoll);
        AddOptionCommand = new Command(AddOption);
        Vpvm = vp;
    }
    public bool ValidatePollDetails()
    {
        var re = new Regex("""^\s*$""");
        bool optionsInvalid = false;
        foreach (var opt in Options)
        {
            if (re.IsMatch(opt.Text))
            {
                optionsInvalid = true;
                break;
            }
        }
        var allOptionTexts = Options.Select(opt => opt.Text);
        if (allOptionTexts.Count() != allOptionTexts.Distinct().Count()) optionsInvalid = true;

        if (re.IsMatch(Title) ||
            re.IsMatch(Description) ||
            optionsInvalid)
        {
            Application.Current.MainPage.DisplayAlert("Invalid poll", "Make sure all fields are filled, and no two options are the same.", "Ok");
            return false;
        }
        return true;
    }

    public void AddOption()
    {
        Options.Add(new VoteOption(""));
        Notify(nameof(Options));
    }

    public void CreatePoll()
    {
        if (ValidatePollDetails())
        {
            var newPoll = new Poll(Vpvm.NextPollId,Vpvm.CurrentUser.Id, Title, Description, ClosedAt, Options);
            Vpvm.OpenPollList.Add(new PollViewModel(newPoll, Vpvm));
            Vpvm.Model.PollList.Add(newPoll);
            DataSerializer.SerializeVotingPlatform(Vpvm.Model);
            ResetAllFields();
            Application.Current.MainPage.DisplayAlert("Success", "", "Ok");
        }
    }
    private void ResetAllFields()
    {
        Title = "";
        Description = "";
        CreatedAt = DateTime.Now;
        ClosedAt = DateTime.Now.AddDays(1);
        Options = [new VoteOption(""), new VoteOption("")];
        NotifyAllPropertiesChanged();
    }
    
    private void NotifyAllPropertiesChanged()
    {
        Notify(nameof(Title));
        Notify(nameof(Description));
        Notify(nameof(Options));
    }
}