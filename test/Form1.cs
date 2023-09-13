using GDataHub;
using GDataHub.Utils;
using System.Configuration;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                IDataBase db;
                string connString = ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString.ToString();

                db = DataBaseSwitcher.GetDataBase(Util.DBType.SQL, connString);
                dgTestGrid.DataSource = db.ExecuteQueryDataTable("SELECT GETDATE()");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }



        }
    }
}