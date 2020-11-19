using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace BruteForceHash
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string eingabe = "";
        string pruf = "1";
        string ausgabe = "";
        string ausgabee = "";


        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnBrechen_Click(object sender, RoutedEventArgs e)
        {
            eingabe = txtHashEingabe.Text;
            if (eingabe == "Hier hash eingeben" || eingabe.Length != 64)
            {
                MessageBox.Show("geben sie einen zu brechenden Hash an!");
            }
            else
            {
                breakHash(eingabe);
            }
        }

        private void breakHash(string angabe)
        {
            for (int i = 0; i < 62; i++)
            {
                rekursivThing(i);
                ausgabee = hashAString(ausgabe);
                if (ausgabee == eingabe)
                {
                    txtAusgabe.Text = ausgabe;
                }
            }
        }

        #region rekursion
        private void rekursivThing(int increment)
        {
            char c;
            if (increment < 10)
            {
                increment += 48;
                c = (char)increment;
            }
            else if (increment < 36)
            {
                increment += 87;
                c = (char)increment;
            }
            else
            {
                increment += 29;
                c = (char)increment;
            }
            ausgabe = c.ToString();            
        }

        private void rekursivThing(int increment,int step)
        {

        }
        #endregion

        #region Warnsholtscher Hashingprozess
        private string hashAString(string stringToHash)
        {
            string hashString;
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(Encoding.Default.GetBytes(stringToHash));
                hashString = ToHex(hash, false);
            }

            return hashString;
        }

        private static string ToHex(byte[] bytes, bool upperCase)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);
            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));
            return result.ToString();
        }
        #endregion
    }
}
