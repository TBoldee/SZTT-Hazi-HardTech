using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingPlatform.ViewModel;

namespace VotingPlatform.View;

public partial class EditPollView : ContentPage
{
    public PollEditViewModel ViewModel { get; set; }
    public EditPollView()
    {
        ViewModel = AppShell.VPVM.PollEditViewModel;
        BindingContext = ViewModel;
        ViewModel.EditSuccessful += MoveToUserList;
        ViewModel.EditFailed += MoveToUserList;
        InitializeComponent();
    }
    
    private async void MoveToUserList()
    {
        await Shell.Current.GoToAsync("//"+nameof(UserPollsView));
    }
    
    private async void EditFailed()
    {
        await DisplayAlert("Edit failed", "Make sure all fields are filled, and no two options are the same.", "Ok");
    }
}