using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;

namespace usersApp.CustomControls
{
    public partial class AuthRegTextBoxControl : UserControl
    {
        public AuthRegTextBoxControl()
        {
            InitializeComponent();
        }
        public string HintText
        {
            get { return (string)GetValue(HintTextProperty); }
            set { SetValue(HintTextProperty, value); }
        }
        public static readonly DependencyProperty HintTextProperty =
            DependencyProperty.Register("HintTextProperty", typeof(string), typeof(AuthRegTextBoxControl), new PropertyMetadata(null, OnHintTextPropertyChanged));

        private static void OnHintTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AuthRegTextBoxControl source = d as AuthRegTextBoxControl;
            HintAssist.SetHint(source.textBoxInput, (string)e.NewValue);
        }
        private void textBoxInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            textBoxInput.Background = Brushes.Transparent;
            textBoxInput.ToolTip = null;
        }
    }
}
