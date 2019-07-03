using System.Windows;
using System.Windows.Documents;
using GitBugger;
using HandyControl.Interactivity;
using WebPConverter.Code;

namespace WebPConverter.Views {
    /// <summary>
    /// Interaction logic for BugsView.xaml
    /// </summary>
    public partial class BugsView  {
        public BugsView() {
            InitializeComponent();
        }
        private void SubmitButton_OnClick(object sender, RoutedEventArgs e) {
            string bodyText = new TextRange(BodyText.Document.ContentStart, BodyText.Document.ContentEnd).Text;

            Bugger bugger = new Bugger("WebPClient", Token.GitToken);
            bugger.CreateIssue("MrSquirrelyNet", "WebPConverter", bugger.Issue(TitleText.Text, bodyText), Bugger.Label.Bug);

            SetClosed();
            SubmitButton.Command.Execute(ControlCommands.Close);
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e) {
            SetClosed();
            SubmitButton.Command.Execute(ControlCommands.Close);
        }

        private static void SetClosed() => Reference.IsBugsOpen = false;
    }
}
