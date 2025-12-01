using VotingPlatform.ViewModel;

namespace VotingPlatform
{
    public partial class AppShell : Shell
    {
        public static VotingPlatformViewModel VPVM { get; } = new VotingPlatformViewModel(new Model.VotingPlatform());
        public AppShell()
        {
            InitializeComponent();
        }
    }
}
