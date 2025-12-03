using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingPlatform.ViewModel;

namespace VotingPlatform.View;

public partial class UserPollsView : ContentPage
{
    public VotingPlatformViewModel ViewModel {get;set;}

    public UserPollsView()
    {
        ViewModel = AppShell.VPVM;
        BindingContext = ViewModel;
        ViewModel.EditClicked += MoveToEditor;
        ViewModel.RefreshHighlights();
        InitializeComponent();
    }
    
    public async void MoveToEditor()
    {
        await Shell.Current.GoToAsync("//"+nameof(EditPollView));
    }
}