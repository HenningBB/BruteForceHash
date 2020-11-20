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
        string ausgabeHash = "";
        bool broken = false;
        string eingabe2="";


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
                txtAusgabe.Text = "Berechne!";
                ausgabe = "";
                broken = false;
                rekursion(cmbLength.SelectedIndex+1, 1);
            }
        }

        private void rekursion(int length, int level)
        {
            if (!broken)
            {
                if (length == level)
                {
                    breakHash();
                }
                else
                {
                    for (int i = 0; i < 62; i++)
                    {
                        string angabe = intToString(i);

                        if (level == 1)
                        {
                            ausgabe = angabe;
                        }
                        else
                        {
                            if (ausgabe.Length < level)
                            {
                                ausgabe += angabe;
                            }
                            else
                            {
                                char[] ergabe = ausgabe.ToCharArray();
                                ergabe[level - 1] = intToChar(i);
                                ausgabe = "";
                                foreach (var item in ergabe)
                                {
                                    if (ausgabe.Length == 0)
                                    {
                                        ausgabe = item.ToString();
                                    }
                                    else
                                    {
                                        ausgabe += item.ToString();
                                    }
                                }
                            }
                        }
                        rekursion(length, level + 1);
                    }
                }
            }
        }



        private void breakHash()
        {
            for (int i = 0; i < 62; i++)
            {
                string angabe = ausgabe;
                angabe += intToString(i);
                ausgabeHash = hashAString(angabe);
                if (ausgabeHash == eingabe)
                {
                    txtAusgabe.Text = angabe;
                    broken = true;
                    break;
                }
                else
                {
                    txtAusgabe.Text = "Kein Ergebnis!";
                }
            }
        }

        private string intToString(int increment)
        {
            char c;
            //Zahlen unicodedezimalstelle von 48-57, increment von 0-9
            if (increment < 10)
            {
                increment += 48;
                c = (char)increment;
            }
            //kleinbuchstaben unicodedezimalstelle von 97-122, increment von 10-35
            else if (increment < 36)
            {
                increment += 87;
                c = (char)increment;
            }
            //großbuchstaben unicodedzimalstelle von 65-90, increment von 36-62
            else
            {
                increment += 29;
                c = (char)increment;
            }
            return c.ToString();
        }

        private char intToChar(int increment)
        {
            char c;
            //Zahlen unicodedezimalstelle von 48-57, increment von 0-9
            if (increment < 10)
            {
                increment += 48;
                c = (char)increment;
            }
            //kleinbuchstaben unicodedezimalstelle von 97-122, increment von 10-35
            else if (increment < 36)
            {
                increment += 87;
                c = (char)increment;
            }
            //großbuchstaben unicodedzimalstelle von 65-90, increment von 36-62
            else
            {
                increment += 29;
                c = (char)increment;
            }
            return c;
        }

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

        private void btnBrechenTest_Click(object sender, RoutedEventArgs e)
        {
            eingabe2 = txtHashEingabeTest.Text;
            eingabe2 = hashAString(eingabe2);
            txtAusgabe2.Text = eingabe2;
        }
    }
}
