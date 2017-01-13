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
using System.Xml.Linq;

namespace AutoChanges
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class tabChangesCommon : UserControl
    {
        public tabChangesCommon()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //changes("*.resx");
            changes("*.xaml");
            changes("*.cs");

        }
        public void clearParamResource(string filename)
        {
            bool check = false;
            StreamReader sr = new StreamReader(filename);
            string rows = sr.ReadToEnd();
            sr.Close();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(rows);
            String[] Delete_values = Regex.Split(txtCouper.Text, "\r\n");
            for (int i = 0; i < Delete_values.Length; i++)
            {
                string xId = "/root/data[@name='" + Delete_values[i].ToString() + "']";
                XmlNode root = doc.SelectNodes(xId)[0];
                if (root != null)
                {
                    check = true;
                    XmlNodeList chil = root.ChildNodes;
                    //Remove the title element.
                    foreach (XmlNode x in chil)
                    {
                        root.RemoveChild(x);
                    }
                    root.ParentNode.RemoveChild(root);
                }
            }
            if (check == true)
            {
                StreamWriter sw = new StreamWriter(filename);
                sw.WriteLine(doc.InnerXml);

                sw.Close();
                sw.Dispose();
            }
          
        }
        public void changes(string dinh_dang_file)
        {
            string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            StringBuilder sb = new StringBuilder();
            if(FileNameTextBox.Text == "")
            {
                MessageBox.Show("Chọn đường dẫn tới folder PL");
                return;
            }
            string link_resource = FileNameTextBox.Text;
            if (dinh_dang_file == "*.resx")
            {

                //String[] allfiles = System.IO.Directory.GetFiles(@"C:\Users\hai.vu\Desktop", dinh_dang_file, System.IO.SearchOption.AllDirectories);
                String[] allfiles = System.IO.Directory.GetFiles(link_resource, dinh_dang_file, System.IO.SearchOption.AllDirectories);
                foreach (string txtName in System.IO.Directory.GetFiles(link_resource, dinh_dang_file, System.IO.SearchOption.AllDirectories))
                {
                    //replaceString(txtName);
                    clearParamResource(txtName);
                }
            }
            else
            {
                String[] allfiles = System.IO.Directory.GetFiles(link_resource, dinh_dang_file, System.IO.SearchOption.AllDirectories);
                foreach (string txtName in System.IO.Directory.GetFiles(link_resource, dinh_dang_file, System.IO.SearchOption.AllDirectories))
                {
                    replaceString(txtName);
                }
            }
        }

        //public string getFilePath()
        //{

        //    return "";
        //}
        private void replaceString(String filename)
        {
            try
            {
                bool check = false;
                StreamReader sr = new StreamReader(filename);
                string rows = sr.ReadToEnd();
                sr.Close();
                txtShowLog.Text += filename + "\r\n";
                String[] Old_value = Regex.Split(txtIdOld.Text, "\r\n");
                String[] New_value = Regex.Split(txtIdNew.Text, "\r\n");
                if (Old_value.Length == New_value.Length)
                {
                    txtCouper.Text = "";
                    for (int j = 0; j < Old_value.Length; j++)
                    {
                        if (rows.Contains(Old_value[j].ToString()))
                        {
                            rows = rows.Replace(Old_value[j].ToString(), New_value[j].ToString());
                            txtShowLog.Text += Old_value[j] + " CHANGES TO " + New_value[j].ToString() + "\r\n";
                            check = true;
                        }

                    }
                    if (check == true)
                    {
                        StreamWriter sw = new StreamWriter(filename);
                        sw.WriteLine(rows);

                        sw.Close();
                        sw.Dispose();


                    }
                    else
                    {

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(filename);
                throw ex;
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            string dinh_dang_file = "*.resx";
            string mydocpath = FileNameTextBox.Text;
            StringBuilder sb = new StringBuilder();
            String[] allfiles = System.IO.Directory.GetFiles(mydocpath, dinh_dang_file, System.IO.SearchOption.AllDirectories);
            //String[] allfiles = System.IO.Directory.GetFiles(@"D:\VMS\VESSEL\Vessel\VVE\VVE.PL\VVE.PL.COMMON\VVE.PL.COMMON.RESOURCES", dinh_dang_file, System.IO.SearchOption.AllDirectories);
            foreach (string txtName in System.IO.Directory.GetFiles(mydocpath, dinh_dang_file, System.IO.SearchOption.AllDirectories))
            {
                //replaceString(txtName);
                clearParamResource(txtName);
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
        }
    }
}
