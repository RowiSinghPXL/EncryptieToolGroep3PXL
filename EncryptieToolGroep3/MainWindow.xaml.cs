using ClassLibrary1;
using ClassLibrary1.Helpers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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


namespace EncryptieToolGroep3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string EncryptOrDecryptThis { get; set; }
        public string FileName { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ClassLibrary1.Helpers.Directory.InitDirectory();
            ClassLibrary1.Helpers.CsvAESKeys.InitAESKeyFile();
            CsvRsaKeys3.InitRSAKeyFile();
            
        }

        private void BtnEncrypt_Click(object sender, RoutedEventArgs e)
        {

            //string str = Convert.ToBase64String(ClassLibrary1.RandomGenerator.GenerateRandomNumber(32));
            //TxtUserOutput.Text = str;
         
            if(TxtUserInput.Text == string.Empty)
            {
                TxtUserInput.Text = EncryptOrDecryptThis;
            }
            
            if(LstAesCred.SelectedIndex >= 0)
            {
                byte[] selectedKey = Convert.FromBase64String(CsvAESKeys.Keys[LstAesCred.SelectedIndex].Key);
                byte[] selectedIV = Convert.FromBase64String(CsvAESKeys.Keys[LstAesCred.SelectedIndex].Iv);
                TxtUserOutput.Text = Convert.ToBase64String(AES.EncryptStringToBytes_Aes(TxtUserInput.Text, selectedKey, selectedIV));
            }
            else
            {
                MessageBox.Show("Selecteer een key.");
            }
            
             
            //string UserText = TxtUserInput.Text;


            ///**/
            ////Aes aes = new Aes();
            //AesManaged aesManaged = new AesManaged();

            //string[] keys = new string[10];

            //for(int i = 0; i < 10; i++)
            //{
            //    aesManaged.GenerateKey();
            //    keys[i] = Convert.ToBase64String(aesManaged.Key);
                
            //}


            ///**/

            ////base64convert


            //using (Aes myAes = Aes.Create())
            //{
            //    myAes.GenerateKey();
            //    var testKey = myAes.Key;
            //    string testKeyStr = Convert.ToBase64String(myAes.Key);




            //    byte[] encrypted = EncryptStringToBytes_Aes(UserText, myAes.Key, myAes.IV);
            //    TxtUserOutput.Text = encrypted.ToString();

            //}
        }

        private static void TestAesCBC()
        {
            const string original = "Text to encrypt";
            var aes = new AES();
            var key = RandomGenerator.GenerateRandomNumber(32); //32bytes = 128 bits, (key size = 128,192,256 bits)
            var iv = RandomGenerator.GenerateRandomNumber(16);

            var encrypted = aes.Encrypt(Encoding.UTF8.GetBytes(original), key, iv);
            var decrypted = aes.Decrypt(encrypted, key, iv);

            var decryptedMessage = Encoding.UTF8.GetString(decrypted);

            var or = original;
            var encr = Convert.ToBase64String(encrypted);
            var decry = decryptedMessage;

        }

        private void BtnGenerateKey_Click(object sender, RoutedEventArgs e)
        {
            //if (CsvAESKeys.FilePath.Equals(""))
            //{
            //    MessageBox("Create")
            //}


            var key = RandomGenerator.GenerateRandomNumber(32); //32bytes = 128 bits, (key size = 128,192,256 bits)
            var iv = RandomGenerator.GenerateRandomNumber(16);

            string keyStr = Convert.ToBase64String(key);
            string ivStr = Convert.ToBase64String(iv);

            CsvAESKeys.AESKeyaddRecord(TxtKeyName.Text, keyStr, ivStr);
            CsvAESKeys.Keys.Add(new ClassLibrary1.Model.AESKey {Name = TxtKeyName.Text, Key = keyStr, Iv = ivStr });
            LstAesCred.ItemsSource = CsvAESKeys.GetAESKeyRecords();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CsvAESKeys.InitKeyRecords();
            CsvRsaKeys3.InitKeyRecords();
            LstAesCred.ItemsSource = CsvAESKeys.GetAESKeyRecords();
            LstRsaCred.ItemsSource = CsvRsaKeys3.GetRSAKeyRecords();
        }

      

        private void LstAesCred_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = LstAesCred.SelectedIndex;
            var itemz = LstAesCred.SelectedItem;
            //var key = CsvAESKeys.Keys[item];
            
        }

        private void BtnGetTextFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string txtFileTextsss = File.ReadAllText(openFileDialog.FileName);
                EncryptOrDecryptThis = txtFileTextsss;
                if (openFileDialog.FileName.Contains('\\'))
                {
                    var helpArr = openFileDialog.FileName.Split('\\');
                    string fileName = helpArr[helpArr.Length - 1];
                    
                    MessageBox.Show("File " + fileName + " selected.");
                }
                else
                {
                    MessageBox.Show("File " + openFileDialog.FileName + " selected.");
                }
                
            }

            
               
        }

        private void BtnDecrypt_Click(object sender, RoutedEventArgs e)
        {
            if (LstAesCred.SelectedIndex >= 0)
            {
                byte[] selectedKey = Convert.FromBase64String(CsvAESKeys.Keys[LstAesCred.SelectedIndex].Key);
                byte[] selectedIV = Convert.FromBase64String(CsvAESKeys.Keys[LstAesCred.SelectedIndex].Iv);
                if(TxtUserOutput.Text != null)
                {
                    byte[] ciperText = Convert.FromBase64String(TxtUserOutput.Text);
                    (string text, bool result) = AES.DecryptStringFromBytes_Aes(ciperText, selectedKey, selectedIV);
                    TxtUserInput.Text = text;
                }
                
                
            }
            else
            {
                MessageBox.Show("Selecteer een key.");
            }
        }

        private void BtnWriteToFile_Click(object sender, RoutedEventArgs e)
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"; // or just "txt files (*.txt)|*.txt" if you only want to save text files
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog1.FileName))
                {
                    if (LstAesCred.SelectedIndex >= 0)
                    {
                        byte[] selectedKey = Convert.FromBase64String(CsvAESKeys.Keys[LstAesCred.SelectedIndex].Key);
                        byte[] selectedIV = Convert.FromBase64String(CsvAESKeys.Keys[LstAesCred.SelectedIndex].Iv);
                        writer.WriteAsync(Convert.ToBase64String(AES.EncryptStringToBytes_Aes(EncryptOrDecryptThis, selectedKey, selectedIV)));
                    }
                    else
                    {
                        MessageBox.Show("Selecteer een key.");
                    }
                    writer.Close();
                }
            }

        }

        private void BtnWriteToFileDecrypt_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt"; // or just "txt files (*.txt)|*.txt" if you only want to save text files
            //saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog1.FileName))
                {
                    if (LstAesCred.SelectedIndex >= 0)
                    {
                        byte[] selectedKey = Convert.FromBase64String(CsvAESKeys.Keys[LstAesCred.SelectedIndex].Key);
                        byte[] selectedIV = Convert.FromBase64String(CsvAESKeys.Keys[LstAesCred.SelectedIndex].Iv);
                        byte[] cipherText = Convert.FromBase64String(EncryptOrDecryptThis);
                        (string text, bool result) = AES.DecryptStringFromBytes_Aes(cipherText, selectedKey, selectedIV);
                        writer.WriteAsync(text);
                        if (!result)
                        {
                            MessageBox.Show("Er ging iets mis tijdens het decrypteren.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selecteer een key.");
                    }
                    writer.Close();
                }
            }
        }

        private void BtnGenerateRSAKey_Click(object sender, RoutedEventArgs e)
        {
            bool iterationCheckInt = int.TryParse(TxtRsaIteration.Text, out int iteration);
            string pwd = TxtRsaKeypwd.Text;
            var Rsa = new RsaHelper();
            string privateKey = Rsa.ExportPrivateKeyString(iteration, pwd);
            string publicKey = Rsa.ExportPublicKeyString();


            //CsvRsaKeysUpdated.AESKeyaddRecord(TxtKeyName.Text, keyStr, ivStr);
            //CsvRsaKeysUpdated.Keys.Add(new ClassLibrary1.Model.AESKey {Name = TxtKeyName.Text, Key = keyStr, Iv = ivStr });
            //LstAesCred.ItemsSource = CsvRsaKeysUpdated.GetAESKeyRecords();

            CsvRsaKeys3.RSAKeyaddRecord(TxtRsakeyname.Text, publicKey, privateKey);
            CsvRsaKeys3.Keys.Add(new ClassLibrary1.Model.RSAKey { Naam = TxtRsakeyname.Text, PrivateKey = privateKey, PublicKey = publicKey });
            LstRsaCred.ItemsSource = CsvRsaKeys3.GetRSAKeyRecords();



        }

        private void BtnEncryptRsa_Click(object sender, RoutedEventArgs e)
        {
            string tekst = TxtUserInputRsa.Text;
            var rsa = new RsaHelper();
            //rsa.ImportPublicKey();


            if (LstRsaCred.SelectedIndex >= 0)
            {
                byte[] selectedPublicKey = Convert.FromBase64String(CsvRsaKeys3.Keys[LstRsaCred.SelectedIndex].PublicKey);
                byte[] selectedPrivateKey = Convert.FromBase64String(CsvRsaKeys3.Keys[LstRsaCred.SelectedIndex].PrivateKey);
                

                rsa.ImportPublicKey(selectedPublicKey);
                var userInput = Convert.FromBase64String(TxtUserInputRsa.Text);
                var encryption = rsa.Encrypt(userInput);
                TxtUserOutputRsa.Text = Convert.ToBase64String(encryption);
            }
            else
            {
                MessageBox.Show("Selecteer een key.");
            }
        }

        private void BtnGetTextFileRsa_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string txtFileTextsss = File.ReadAllText(openFileDialog.FileName);
                FileName = openFileDialog.FileName;
                EncryptOrDecryptThis = txtFileTextsss;
                if (openFileDialog.FileName.Contains('\\'))
                {
                    var helpArr = openFileDialog.FileName.Split('\\');
                    string fileName = helpArr[helpArr.Length - 1];

                    MessageBox.Show("File " + fileName + " selected.");
                }
                else
                {
                    MessageBox.Show("File " + openFileDialog.FileName + " selected.");
                }

            }
        }

        private void BtnWriteToFileRsa_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"; // or just "txt files (*.txt)|*.txt" if you only want to save text files
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog1.FileName))
                {
                    if (LstRsaCred.SelectedIndex >= 0)
                    {
                        byte[] privateKey = Convert.FromBase64String(CsvRsaKeys3.Keys[LstRsaCred.SelectedIndex].PrivateKey);
                        byte[] publicKey = Convert.FromBase64String(CsvRsaKeys3.Keys[LstRsaCred.SelectedIndex].PublicKey);
                        var rsa = new RsaHelper();
                        rsa.ImportPublicKey(publicKey);

                        //var bytesz = Convert.FromBase64String(EncryptOrDecryptThis);
                        byte[] bytes = System.IO.File.ReadAllBytes(FileName);
                        var encrypted = rsa.Encrypt(bytes);
                        writer.WriteAsync(Convert.ToBase64String(encrypted));
                    }
                    else
                    {
                        MessageBox.Show("Selecteer een key.");
                    }
                    writer.Close();
                }
            }
        }

        private void BtnWriteToFileDecryptRsa_Click(object sender, RoutedEventArgs e)
        {

        }



        //static byte[] EncryptStringToBytes_Aes(string userText, byte[] Key, byte[] IV)
        //{
        //    // check user inputs
        //    if (userText == null || userText.Length <= 0)
        //        throw new ArgumentNullException("plainText");
        //    if (Key == null || Key.Length <= 0)
        //        throw new ArgumentNullException("Key");
        //    if (IV == null || IV.Length <= 0)
        //        throw new ArgumentNullException("IV");
        //    byte[] encrypted;

        //    using (Aes aesAlg = Aes.Create())
        //    {
        //        aesAlg.Key = GenerateAesKey.AesKey();
        //        aesAlg.IV = GenerateAesKey.AesIv();

        //        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        //        using (MemoryStream msEncrypt = new MemoryStream())
        //        {
        //            using(CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        //            {
        //                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
        //                {
        //                    swEncrypt.Write(userText);
        //                }
        //                encrypted = msEncrypt.ToArray();
        //            }
        //        }
        //    }

        //    return encrypted;
        //}
    }
}
