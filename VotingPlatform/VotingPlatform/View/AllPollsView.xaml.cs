using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingPlatform.ViewModel;

namespace VotingPlatform.View;

public partial class AllPollsView : ContentPage
{
    public VotingPlatformViewModel ViewModel {get;set;}
    public AllPollsView()
    {
        ViewModel = AppShell.VPVM;
        BindingContext = ViewModel;
        ViewModel.RefreshHighlights();
        InitializeComponent();
    }
}