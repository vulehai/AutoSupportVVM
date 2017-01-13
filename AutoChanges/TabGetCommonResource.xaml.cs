using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Xml;

namespace AutoChanges
{
    /// <summary>
    /// Interaction logic for TabGetCommonResource.xaml
    /// </summary>
    public partial class TabGetCommonResource : UserControl
    {
        public TabGetCommonResource()
        {
            InitializeComponent();
        }
        public void getId(string filename)
        {
            bool check = false;
           
            StreamReader sr = new StreamReader(filename);
            string rows = sr.ReadToEnd();
            sr.Close();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(rows);
            String[] Value_List = Regex.Split(txtValue.Text.Trim(), "\r\n");
            for (int i = 0; i < Value_List.Length; i++)
            {
                bool check_value = false;
                string xId = "/root/data/value";
                XmlNodeList root = doc.SelectNodes(xId);
                if (root != null)
                {
                    foreach (XmlNode x in root)
                    {
                        if (x.InnerText == Value_List[i].Trim())
                        {
                            check_value = true;
                            XmlElement data = (XmlElement)x.ParentNode;
                           
                                txtID.Text += data.GetAttribute("name") + "\r\n";
                               
                            
                            break;
                        }
                    }
                    
                }
                if(check_value == true)
                {
                    txtNewValue.Text += "\r\n";
                }
                else
                {
                    txtNewValue.Text += Value_List[i].ToString() + "\r\n";
                    txtID.Text += "\r\n";
                }
            }
            //if (check == true)
            //{
            //    StreamWriter sw = new StreamWriter(filename);
            //    sw.WriteLine(doc.InnerXml);

            //    sw.Close();
            //    sw.Dispose();
            //}

        }
        private void btnGetID_Click(object sender, RoutedEventArgs e)
        {
            if (FileNameTextBox.Text == "") {
                MessageBox.Show("Chọn folder kìa chế ơi!");
                return;
            }
            txtID.Text = "";
            txtNewValue.Text = "";
            string dinh_dang_file = "*.resx";
            string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string path = FileNameTextBox.Text;
            StringBuilder sb = new StringBuilder();

            String[] allfiles = System.IO.Directory.GetFiles(path, dinh_dang_file, System.IO.SearchOption.AllDirectories);
            foreach (string txtName in System.IO.Directory.GetFiles(path, dinh_dang_file, System.IO.SearchOption.AllDirectories))
            {
                string path_file = "";
                if (LBL_NAME.IsChecked == true)
                {
                    path_file = path + "\\LBL.resx";
                }else{
                    path_file = path + "\\BTN.resx";
                }
                //replaceString(txtName);
                if (txtName == path_file)
                {
                    getId(path_file);

                }
            }
            MessageBox.Show("Delete done!");
        }
      
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            var result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
               // FileNameTextBox.Text = result.ToString();
                FileNameTextBox.Text = dialog.SelectedPath;
            }
           
            //Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            //// Set filter for file extension and default file extension
            ////dlg.DefaultExt = ".txt";
            ////dlg.Filter = "Text documents (.txt)|*.txt";
            //dlg.Filter = "Test *.*|*.*";
          
            //// Display OpenFileDialog by calling ShowDialog method
            //Nullable<bool> result = dlg.ShowDialog();

            //// Get the selected file name and display in a TextBox
            //if (result == true)
            //{
            //    // Open document
            //    string filename = dlg.FileName;
            //    FileNameTextBox.Text = filename;
            //}
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void BTN_NAME_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
