using System.Windows;

namespace HavenheimManager.Editors
{
    /// <summary>
    /// Interaction logic for InputBox.xaml
    /// </summary>
    public partial class InputBox : Window
    {
        public string RequestedElement { get; set; }

        public string Input { get; set; }

        public DelegateCommand AcceptInputCommand { get; }
        public DelegateCommand CancelCommand { get; }

        public InputBox(string requestedElement)
        {
            AcceptInputCommand = new DelegateCommand(AcceptInputAction);
            CancelCommand = new DelegateCommand(CancelAction);
            RequestedElement = requestedElement;
            this.DataContext = this;

            InitializeComponent();
        }

        public string GetInput() => Input;

        private void AcceptInputAction(object arg)
        {
            if (arg is Window window)
            {
                if (!string.IsNullOrWhiteSpace(Input))
                {
                    window.DialogResult = true;
                    window.Close();
                }
                else
                {
                    window.DialogResult = false;
                    window.Close();
                }
            }
        }

        private void CancelAction(object arg)
        {
            if (arg is Window window)
            {
                window.DialogResult = false;
                window.Close();
            }
        }
    }
}
