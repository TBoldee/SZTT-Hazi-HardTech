using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingPlatform.ViewModel;

namespace VotingPlatform.View;

public partial class OpenPollsView : ContentPage
{
    public VotingPlatformViewModel ViewModel {get;set;}
    public OpenPollsView()
    {
        ViewModel = AppShell.VPVM;
        BindingContext = ViewModel;
        ViewModel.RefreshHighlightsAsync();
        InitializeComponent();
    }
}