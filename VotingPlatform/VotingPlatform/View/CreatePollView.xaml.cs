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
        InitializeComponent();
    }
}