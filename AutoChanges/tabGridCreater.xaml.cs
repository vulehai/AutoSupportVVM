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

namespace AutoChanges
{
    /// <summary>
    /// Interaction logic for tabGridCreater.xaml
    /// </summary>
    public partial class tabGridCreater : UserControl
    {
        string resultInitCol = "";
        string functionGridInitStart = "private void metInitializeGrid()\n{\n\ttry\n\t{";
        string functionGridInitEnd = "\n\t}\n\tcatch (Exception ex)\n\t{\n\t\tthrow ex;\n\t}\n}";
        string functionCreateTableStart = " private DataTable metTableSchemaDefineGrid()\n{\n\tDataTable tDt = null;\n\ttry\n\t{\n\t\tDictionary<string, Type> tDicUser = new Dictionary<string, Type>();\n";
        string functionCreateTableEnd = "\n\t}\n\tcatch (Exception ex)\n\t{\n\t\tthrow ex;\n\t}\n}";
        string functionCreateDataStart = "private void metGetGridData()\n{\n\tthis.eGridDataSource = metTableSchemaDefineGrid();";
        string functionCreateDataEnd = "\n\tthis.eGridHelper.BindItemSource(this.eGridDataSource);\n}";
        public tabGridCreater()
        {
            InitializeComponent();
        }

        private void btnGen_Click(object sender, RoutedEventArgs e)
        {
            string[] headerNm = txtHeader.Text.Trim().Split(new[] { "\r\n" }, StringSplitOptions.None);
            string[] resNm = txtResource.Text.Trim().Split(new[] { "\r\n" }, StringSplitOptions.None);
            string[] tpNm = txtColType.Text.Trim().Split(new[] { "\r\n" }, StringSplitOptions.None);
            string[] colMg = txtColMargin.Text.Trim().Split(new[] { "\r\n" }, StringSplitOptions.None);
            string col = "";
            string table = "";
            string data = "";
            string dataRow = "\n\t\tthis.eGridDataSource.Rows.Add(new object[] { ";

            string resultInitCol = "\t\t  this.eGridHelper = new GridHelper(grd, tbv, true); \n\t\t Column tColumn; \n\t\t";

            if (((headerNm.Length + resNm.Length) - (tpNm.Length + colMg.Length)) != 0)
            {
                MessageBoxResult result = MessageBox.Show("Nhầm rùi", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    MessageBoxResult result1 = MessageBox.Show("Coi lại đê", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Question);
                }
                return;
            }
            col += "this.eGridHelper = new GridHelper(grd, view, true, true, true);";
            col += "\n\t\tColumn tColumn = null;";
              
            for (int i = 0; i < headerNm.Length; i++ )
            {
                col += "\n\n\t\ttColumn = new Column(\"" + headerNm[i].ToUpper().Replace(" ", "").Replace(".", "") + "\",LBL." + resNm[i] + ");\\\\" + headerNm[i].Trim() ;
                col += "\n\t\ttColumn.Width = 120;";
                col += "\n\t\ttColumn.Visible = true;\n\t\ttColumn.AllowEditing = DevExpress.Utils.DefaultBoolean.True;";

                col += "\n\t\ttColumn.HorizontalContentAlignment = DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment."+UppercaseFirst(colMg[i].ToLower())+";";
                col += "\n\t\teGridHelper.Add(tColumn);";
                table += "\n\t\ttDicUser.Add(\""+ headerNm[i].ToUpper().Replace(" ", "").Replace(".", "")+"\", typeof(string));";
                
                dataRow += "\""+headerNm[i]+"\",";
            }
            col += "\n\t\tthis.eGridHelper.Initialize();";
            table += "\n\t\treturn tDt = TableUtil.ConvertDictionaryToTable(tDicUser, false, false);";
            dataRow += "});";

            for (int i = 0; i < 3; i ++ )
            {
                data += dataRow;
            }
            this.txtResult.Text = functionGridInitStart + col + functionGridInitEnd;
            this.txtResult.Text += "\n" + functionCreateTableStart + table + functionCreateTableEnd;
            this.txtResult.Text += "\n" + functionCreateDataStart+data + functionCreateDataEnd;
        }
        static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            this.resultInitCol = "";
            this.txtHeader.Text = "";
            this.txtResource.Text = "";
            this.txtColType.Text = "";
            this.txtColMargin.Text = "";
            this.txtResult.Text = "";
        }
    }
}
