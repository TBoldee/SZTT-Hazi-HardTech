using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingPlatform.ViewModel;

namespace VotingPlatform.View;

public partial class CreatePollView : ContentPage
{
    public PollCreationViewModel ViewModel {get; set;}
    public CreatePollView()
    {
        this.ViewModel = AppShell.VPVM.PollCreationViewModel;
        BindingContext = this.ViewModel;
        ViewModel.CreationFailed += DisplayFail;
        ViewModel.CreationSucceeded += DisplaySuccess;
        InitializeComponent();
    }

    public async void DisplayFail()
    {
        await DisplayAlert("Invalid poll", "Make sure all fields are filled, and no two options are the same.", "Ok");
    }

    public async void DisplaySuccess()
    {
        await DisplayAlert("Success", "", "Ok");
    }
}