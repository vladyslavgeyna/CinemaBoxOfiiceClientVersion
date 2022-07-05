using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using usersApp.Classes.CinemaClasses;
using usersApp.Classes.DataBaseClasses;
using usersApp.Pages;
using usersApp.Windows;
using XamlAnimatedGif;

namespace usersApp.CustomControls
{
    public partial class ProductControl : UserControl
    {
        public ProductControl()
        {
            InitializeComponent();
        }
        public ImageSource ProductImageSource  
        {
            get { return (ImageSource)GetValue(ProductImageSourceProperty); }
            set { SetValue(ProductImageSourceProperty, value); }
        }
        public static readonly DependencyProperty ProductImageSourceProperty =
            DependencyProperty.Register("ProductImageSourceProperty", typeof(ImageSource), typeof(ProductControl), new PropertyMetadata(null, OnProductImageSourcePropertyChanged));
        private static void OnProductImageSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProductControl source = d as ProductControl;
            Uri uri = new Uri(e.NewValue.ToString());
            AnimationBehavior.SetSourceUri(source.ImageProduct, uri);
        }
        public string ProductName
        {
            get { return (string)GetValue(ProductNameProperty); }
            set { SetValue(ProductNameProperty, value); }
        }
        public static readonly DependencyProperty ProductNameProperty =
            DependencyProperty.Register("ProductNameProperty", typeof(string), typeof(ProductControl), new PropertyMetadata(null, OnProductNamePropertyChanged));

        private static void OnProductNamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProductControl source = d as ProductControl;
            source.TextBlockProductName.Text = (string)e.NewValue;
        }
        public string ProductPrice
        {
            get { return (string)GetValue(ProductPriceProperty); }
            set { SetValue(ProductPriceProperty, value); }
        }
        public static readonly DependencyProperty ProductPriceProperty =
            DependencyProperty.Register("ProductPriceProperty", typeof(string), typeof(ProductControl), new PropertyMetadata(null, OnProductPricePropertyChanged));
        private static void OnProductPricePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProductControl source = d as ProductControl;
            source.TextBlockProductPrice.Text = (string)e.NewValue + " грн";
        }
        private int count { get; set; }
        private void ButtonProductInc_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ButtonProductDec.IsEnabled = true;
                count = int.Parse(TextBlockProductCount.Text);
                count++;
                TextBlockProductCount.Text = count.ToString();
            }
            catch (Exception)
            {
                TextBlockProductCount.Text = "0";
                ButtonProductDec.IsEnabled = false;
            }
        }
        private void ButtonProductDec_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                count = int.Parse(TextBlockProductCount.Text);
                count--;
                TextBlockProductCount.Text = count.ToString();
                if (TextBlockProductCount.Text == "0")
                    ButtonProductDec.IsEnabled = false;
            }
            catch (Exception)
            {
                TextBlockProductCount.Text = "0";
                ButtonProductDec.IsEnabled = false;
            }
        }
        private AppContext db;
        private Product product;
        private CinemaShopProduct cinemaShopProduct;
        private async void ButtonToFavorite_Click(object sender, RoutedEventArgs e)
        {
            if(TextBlockProductCount.Text == "" || int.Parse(TextBlockProductCount.Text) <= 0)
            {
                new MesBox("Помилка в кількості обраного вами товару", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }
            else
            {
                if (AuthWindow.authUser != null)
                {
                    cinemaShopProduct = new CinemaShopProduct(ProductName, int.Parse(ProductPrice), int.Parse(TextBlockProductCount.Text));
                    db = AppContext.GetInstance();
                    product = new Product(cinemaShopProduct.Name, cinemaShopProduct.Price, AuthWindow.authUser.id, cinemaShopProduct.Amount);
                    db.Products.Add(product);
                    db.SaveChanges();
                    UserPageWindow.navigationPages[2] = new FavouritePage();
                    Uri uriSuccess = new Uri("pack://application:,,,/Images/success.gif");
                    AnimationBehavior.SetSourceUri(ImageProduct, uriSuccess);
                    await Task.Factory.StartNew(() => Pause(3));
                    Uri uriCurrent = new Uri(ProductImageSource.ToString());
                    AnimationBehavior.SetSourceUri(ImageProduct, uriCurrent);
                    TextBlockProductCount.Text = "0";
                    ButtonProductDec.IsEnabled = false;
                }
            }
        }
        private void Pause(int T)
        {
            Thread.Sleep(T * 1000);
        }
        private void TextBlockProductCount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {   
            if (e.Text == "0")
                ButtonProductDec.IsEnabled = false;
            else
                ButtonProductDec.IsEnabled = true;
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
