using VotingPlatform.Model;
using VotingPlatform.ViewModel;

namespace VotingPlatform
{
    public partial class AppShell : Shell
    {
        public static VotingPlatformViewModel VPVM { get; } = new VotingPlatformViewModel(DataSerializer.DeserializeVotingPlatform());
        public AppShell()
        {
            InitializeComponent();
		}
    }
}
