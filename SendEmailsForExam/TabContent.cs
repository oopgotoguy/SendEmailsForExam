using System;
using System.IO;
using System.Windows.Forms;

namespace SendEmailsForExam
{
    public partial class TabContent : UserControl
    {
        public string TabName;
        MainForm mnf;
        public TabContent(string tabName)
        {
            InitializeComponent();
            TabName = tabName;
            mnf = Application.OpenForms["MainForm"] as MainForm;
        }

        private void btnDeleteTab_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure and did you save other tabs? this operation will load the other tabs again", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var filePath = $"{mnf.SavingDirectory}\\{TabName}.txt";
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    mnf.LoadData();
                }
            }
        }
    }
}
