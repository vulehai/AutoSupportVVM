//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
//using System.Xml;
//using System.Xml.Linq;

//namespace AutoChanges
//{
//    /// <summary>
//    /// Interaction logic for MainWindow.xaml
//    /// </summary>
//    public partial class MainWindow : Window
//    {
//        public MainWindow()
//        {
//            InitializeComponent();
//        }

//        private void Button_Click(object sender, RoutedEventArgs e)
//        {
//            changes("*.resx");
//            // changes("*.xaml");
//            // changes("*.cs");

//        }
//        public void clearParamResource(string filename)
//        {
//            StreamReader sr = new StreamReader(filename);
//            string rows = sr.ReadToEnd();
//            sr.Close();
//            XmlDocument doc = new XmlDocument();
//            doc.LoadXml(rows);
//            string xfile = "/root/data[@name='" + txtCouper.Text + "']";
//            XmlNode root = doc.SelectNodes(xfile)[0];
//            XmlNodeList chil = root.ChildNodes;
//            //Remove the title element.
//            foreach (XmlNode x in chil)
//            {
//                root.RemoveChild(x);
//            }
//            root.ParentNode.RemoveChild(root);

//            StreamWriter sw = new StreamWriter(filename);
//            sw.WriteLine(doc.InnerXml);

//            sw.Close();
//            sw.Dispose();
//        }
//        public void changes(string dinh_dang_file)
//        {
//            string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
//            StringBuilder sb = new StringBuilder();
//            if (dinh_dang_file == "*.resx")
//            {

//                String[] allfiles = System.IO.Directory.GetFiles(@"C:\Users\hai.vu\Desktop", dinh_dang_file, System.IO.SearchOption.AllDirectories);
//                //String[] allfiles = System.IO.Directory.GetFiles(@"D:\VMS\VESSEL\Vessel\VVE\VVE.PL\VVE.PL.COMMON\VVE.PL.COMMON.RESOURCES", dinh_dang_file, System.IO.SearchOption.AllDirectories);
//                foreach (string txtName in System.IO.Directory.GetFiles(@"C:\Users\hai.vu\Desktop", dinh_dang_file, System.IO.SearchOption.AllDirectories))
//                {
//                    //replaceString(txtName);
//                    clearParamResource(txtName);
//                }
//            }
//            else
//            {
//                String[] allfiles = System.IO.Directory.GetFiles(@"D:\VMS\VESSEL\Vessel\VVE\VVE.PL", dinh_dang_file, System.IO.SearchOption.AllDirectories);
//                foreach (string txtName in System.IO.Directory.GetFiles(@"D:\VMS\VESSEL\Vessel\VVE\VVE.PL", dinh_dang_file, System.IO.SearchOption.AllDirectories))
//                {
//                    replaceString(txtName);
//                }
//            }
//        }

//        //public string getFilePath()
//        //{

//        //    return "";
//        //}
//        private void replaceString(String filename)
//        {
//            try
//            {
//                bool check = false;
//                StreamReader sr = new StreamReader(filename);
//                string rows = sr.ReadToEnd();
//                sr.Close();
//                txtShowLog.Text += filename + "\r\n";
//                String[] Old_value = Regex.Split(txtIdOld.Text, "\r\n");
//                String[] New_value = Regex.Split(txtIdNew.Text, "\r\n");
//                if (Old_value.Length == New_value.Length)
//                {
//                    txtCouper.Text = "";
//                    for (int j = 0; j < Old_value.Length; j++)
//                    {
//                        if (rows.Contains(Old_value[j].ToString()))
//                        {
//                            rows = rows.Replace(Old_value[j].ToString(), New_value[j].ToString());
//                            txtShowLog.Text += Old_value[j] + " CHANGES TO " + New_value[j].ToString() + "\r\n";
//                            check = true;
//                        }

//                    }
//                    if (check == true)
//                    {
//                        StreamWriter sw = new StreamWriter(filename);
//                        sw.WriteLine(rows);

//                        sw.Close();
//                        sw.Dispose();


//                    }

//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(filename);
//                throw ex;
//            }

//        }
//    }
//}
