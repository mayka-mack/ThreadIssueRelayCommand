using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ThreadIssueRelayCommand
{
    [ObservableObject]
    public partial class MainWindow
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(OpenTemplateCommand))]
        [NotifyCanExecuteChangedFor(nameof(ResetTemplateCommand))]
        private bool _templateBeingModified;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private bool CanModifyTemplate() => !TemplateBeingModified;

        private void GenerateTemplate()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                TemplateBeingModified = true;
            });

            Thread.Sleep(1000); // simulate work

            Application.Current.Dispatcher.Invoke(() =>
            {
                TemplateBeingModified = false;
            });
        }

        [RelayCommand(CanExecute = nameof(CanModifyTemplate))]
        private async Task OpenTemplateAsync()
        {
            await Task.Run(GenerateTemplate);
            // ...
        }

        [RelayCommand(CanExecute = nameof(CanModifyTemplate))]
        private async Task ResetTemplateAsync()
        {
            await Task.Run(GenerateTemplate);
        }
    }
}
