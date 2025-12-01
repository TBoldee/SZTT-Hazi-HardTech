using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingPlatform.View;

public partial class CreatePollView : ContentPage
{
    public CreatePollView()
    {
        InitializeComponent();
    }
    private void AddOptionButtonClicked(object? sender, EventArgs e)
    {
        Options.Children.Add(new Entry(){WidthRequest = 150});
    }

    private void CreatePollButtonClicked(object? sender, EventArgs e)
    {
        
    }
}