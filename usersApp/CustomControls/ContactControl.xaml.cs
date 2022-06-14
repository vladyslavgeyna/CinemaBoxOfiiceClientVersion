using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Diagnostics;

namespace usersApp.CustomControls
{
    public partial class ContactControl : UserControl
    {
        public ContactControl()
        {
            InitializeComponent();
        }
        public ImageSource ContactImageSource
        {
            get { return (ImageSource)GetValue(ContactImageSourceProperty); }
            set { SetValue(ContactImageSourceProperty, value); }
        }
        public static readonly DependencyProperty ContactImageSourceProperty =
            DependencyProperty.Register("ContactImageSourceProperty", typeof(ImageSource), typeof(ContactControl), new PropertyMetadata(null, OnContactImageSourcePropertyChanged));
        private static void OnContactImageSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ContactControl source = d as ContactControl;
            source.ImageContact.Source = (ImageSource)e.NewValue;
        }
        public string ContactName
        {
            get { return (string)GetValue(ContactNameProperty); }
            set { SetValue(ContactNameProperty, value); }
        }
        public static readonly DependencyProperty ContactNameProperty =
            DependencyProperty.Register("ContactNameProperty", typeof(string), typeof(ContactControl), new PropertyMetadata(null, OnContactNamePropertyChanged));
        private static void OnContactNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ContactControl source = d as ContactControl;
            source.TextBlockContactName.Text = (string)e.NewValue;
        }
        private string contactLink;
        public string ContactLink
        {
            get { return contactLink; }
            set { contactLink = value; }
        }
        private void HyperLinkContact_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(contactLink);
        }
    }
}
