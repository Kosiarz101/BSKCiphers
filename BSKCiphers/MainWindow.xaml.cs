using BSKCiphers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BSKCiphers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)CipherCB.SelectedItem;
            try
            {
                ICipher cipher = CreateCipher(item.Name);
                AnswerTB.Text = cipher.Decrypt(WordTB.Text, KeyTB.Text);
            }
            catch (ArgumentException ex)
            {
                AnswerTB.Text = ex.Message;
            }           
        }

        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)CipherCB.SelectedItem;           
            try
            {
                ICipher cipher = CreateCipher(item.Name);
                AnswerTB.Text = cipher.Encrypt(WordTB.Text, KeyTB.Text);
            }
            catch(ArgumentException ex)
            {
                AnswerTB.Text = ex.Message;
            }
        }
        private ICipher CreateCipher(string cipher)
        {      
            if (cipher == "RailFence")
            {
                if (!Int32.TryParse(KeyTB.Text, out int result) || result <= 0)
                {
                    throw new ArgumentException("Key should be a positive integer value");
                }
                else
                {
                    return new RailFenceCipher();
                    
                }
            }
            else if (cipher == "TCA")
            {
                return new TranspositionCipherA();
            }
            else
            {
                return new TranspositionCipherB();
            }
        }
    }
}
