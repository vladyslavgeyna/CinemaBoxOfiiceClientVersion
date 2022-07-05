using System;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Windows;
using usersApp.Classes.CinemaClasses;
using Word = Microsoft.Office.Interop.Word;
using usersApp.Classes.DataBaseClasses;
using SautinSoft.Document;
using usersApp.Pages;

namespace usersApp.Windows
{
    public partial class OrderWindow : Window
    {
        private CinemaOrder cinemaOrder;
        public static bool IsOrderMade = false;
        public OrderWindow()
        {
            InitializeComponent();
        }
        private void buttonCompleteOrder_Click(object sender, RoutedEventArgs e)
        {
            IsOrderMade = true;
            string paymentMethod = "";
            if (radioButtonCardPay.IsChecked == true)
            {
                paymentMethod = radioButtonCardPay.Content.ToString();
            }
            else if (radioButtonCashPay.IsChecked == true)
            {
                paymentMethod = radioButtonCashPay.Content.ToString();
            }
            cinemaOrder = new CinemaOrder(DateTime.Now, paymentMethod);
            CreateWordDocument(BingPathToAppDir("OrderTicketsFiles\\TemporaryFiles\\TemporaryTicket.docx"), BingPathToAppDir($"OrderTicketsFiles\\GeneratedFiles\\CinemaOrder{AuthWindow.authUser.Login}Ticket.docx"));
            //Перетворення в формат .pdf
            //DocumentCore documentCore = DocumentCore.Load($"C:\\Users\\Admin\\Desktop\\CinemaOrder{AuthWindow.authUser.Login}.docx");
            //documentCore.Save($"C:\\Users\\Admin\\Desktop\\CinemaOrder{AuthWindow.authUser.Login}.pdf");
            this.Close();
        }
        private void FindAndReplace(Word.Application wordApp, object toFindText, object replaceWithText)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundLike = false;
            object nmatchAllforms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiactitics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;
            wordApp.Selection.Find.Execute(ref toFindText,
                ref matchCase, ref matchWholeWord,
                ref matchWildCards, ref matchSoundLike,
                ref nmatchAllforms, ref forward,
                ref wrap, ref format, ref replaceWithText,
                ref replace, ref matchKashida,
                ref matchDiactitics, ref matchAlefHamza,
                ref matchControl);
        }
        private void CreateWordDocument(object filename, object SaveAs)
        {
            Word.Application wordApp = new Word.Application();
            object missing = Missing.Value;
            Word.Document myWordDoc = null;
            if (File.Exists((string)filename))
            {
                object readOnly = false;
                object isVisible = false;
                wordApp.Visible = false;
                myWordDoc = wordApp.Documents.Open(ref filename, ref missing, ref readOnly,
                                        ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing, ref missing);
                myWordDoc.Activate();
                this.FindAndReplace(wordApp, "<movie>", FavouritePage.movies.FirstOrDefault().Name);
                this.FindAndReplace(wordApp, "<dateOfSession>", FavouritePage.movies.FirstOrDefault().DayOfSession.ToString() + "." + FavouritePage.movies.FirstOrDefault().MonthOfSession.ToString() + "." + FavouritePage.movies.FirstOrDefault().YearOfSession.ToString());
                this.FindAndReplace(wordApp, "<timeOfSession>", FavouritePage.movies.FirstOrDefault().TimeOfSession);
                this.FindAndReplace(wordApp, "<placeRow>", FavouritePage.movies.FirstOrDefault().RowPlace.ToString());
                this.FindAndReplace(wordApp, "<placeColumn>", FavouritePage.movies.FirstOrDefault().ColumnPlace.ToString());
                this.FindAndReplace(wordApp, "<moviePrice>", FavouritePage.movies.FirstOrDefault().Price.ToString() + "грн");
                if (FavouritePage.products.Count != 0)
                {
                    string tmp = "";
                    foreach (Product product in FavouritePage.products)
                    {
                        tmp += product.Name + " " + product.Price + "грн" + " " + product.Amount + "шт" + Environment.NewLine;
                    }
                    this.FindAndReplace(wordApp, "<productsTitle>", "ТОВАРИ");
                    this.FindAndReplace(wordApp, "<products>", tmp);

                }
                else
                {
                    this.FindAndReplace(wordApp, "<products>", "");
                    this.FindAndReplace(wordApp, "<productsTitle>", "");
                }
                this.FindAndReplace(wordApp, "<totalPrice>", FavouritePage.TotalPrice.ToString() + "грн");
                this.FindAndReplace(wordApp, "<paymentMethod>", cinemaOrder.PaymentMethod);
                this.FindAndReplace(wordApp, "<dateAndTimeOfOrder>", cinemaOrder.DateTimeOfOrder.ToString()); 
                if(FavouritePage.movies.FirstOrDefault().Plan != null)
                {
                    this.FindAndReplace(wordApp, "<planTitle>", "Тариф:");
                    this.FindAndReplace(wordApp, "<plan>", FavouritePage.movies.FirstOrDefault().Plan);
                }
                else
                {
                    this.FindAndReplace(wordApp, "<planTitle>", "");
                    this.FindAndReplace(wordApp, "<plan>", "");
                }
            }
            else
            {
                new MesBox("Файл не знайдено", MessageType.Error, MessageButtons.Ok).ShowDialog();
                return;
            }
            myWordDoc.SaveAs2(ref SaveAs, ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing);
            myWordDoc.Close();
            wordApp.Quit();
            new MesBox("Ваш квиток створено", MessageType.Success, MessageButtons.Ok).ShowDialog();
        }
        private void checkBoxAcceptRules_Click(object sender, RoutedEventArgs e)
        {
            if (checkBoxAcceptRules.IsChecked == true)
            {
                buttonCompleteOrder.IsEnabled = true;
            }
            else
            {
                buttonCompleteOrder.IsEnabled = false;
            }
        }
        private void radioButtonCardPay_Checked(object sender, RoutedEventArgs e)
        {
            if(!checkBoxAcceptRules.IsEnabled)
            {
                checkBoxAcceptRules.IsEnabled = true;
            }
        }
        private void radioButtonCashPay_Checked(object sender, RoutedEventArgs e)
        {
            if (!checkBoxAcceptRules.IsEnabled)
            {
                checkBoxAcceptRules.IsEnabled = true;
            }
        }
        public static string BingPathToAppDir(string localPath)
        {
            string currentDir = Environment.CurrentDirectory;
            DirectoryInfo directory = new DirectoryInfo(Path.GetFullPath(Path.Combine(currentDir, @"..\..\" + localPath)));
            return directory.ToString();
        }
    }
}
