using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendEmailsForExam
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var dirInfo = new DirectoryInfo(SavingDirectory);
            if (!dirInfo.Exists) dirInfo.Create();

            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Person p = new Person("");
            (tabControl1.SelectedTab.Controls[0] as TabContent).pnlTo.Controls.Add(p);
        }
        public string SavingDirectory = Application.StartupPath + "\\Data\\";

        private void BtnSaveData_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.From = txtFrom.Text;
            Properties.Settings.Default.Subject = txtSubject.Text;
            Properties.Settings.Default.Body = txtBody.Text;
            Properties.Settings.Default.Server = txtServer.Text;
            Properties.Settings.Default.Port = txtPort.Text;
            Properties.Settings.Default.Password = txtPassword.Text;
            Properties.Settings.Default.Save();

            if (tabControl1.SelectedIndex == -1) return;

            List<string> lst = new List<string>();
            foreach (var item in (tabControl1.SelectedTab.Controls[0] as TabContent).pnlTo.Controls)
            {
                if (item is Person)
                {
                    var person = item as Person;
                    lst.Add(person.ToString());
                }
            }

            if (lst.Count > 0)
            {
                var savingPath = $"{SavingDirectory}\\{(tabControl1.SelectedTab.Controls[0] as TabContent).TabName}.txt";
                if (File.Exists(savingPath))
                {
                    File.Delete(savingPath);
                }
                File.WriteAllLines(savingPath, lst);
            }
            MessageBox.Show("done");
        }

        public void LoadData()
        {
            txtFrom.Text = Properties.Settings.Default.From;
            txtBody.Text = Properties.Settings.Default.Body;
            txtPassword.Text = Properties.Settings.Default.Password;
            txtServer.Text = Properties.Settings.Default.Server;
            txtSubject.Text = Properties.Settings.Default.Subject;
            txtPort.Text = Properties.Settings.Default.Port;
            tabControl1.Controls.Clear();

            var tabs = Directory.GetFiles(SavingDirectory, "*.txt", SearchOption.TopDirectoryOnly);

            foreach (var savingPath in tabs)
            {
                var tab = new TabPage() { Text = Path.GetFileNameWithoutExtension(savingPath) };
                tab.Invalidated += Tab_Invalidated;
                tabControl1.Controls.Add(tab);
            }
        }

        private void Tab_Invalidated(object sender, InvalidateEventArgs e)
        {
            var tab = (sender as TabPage);
            var savingPath = $"{SavingDirectory}\\{tab.Text}.txt";
            if (File.Exists(savingPath))
            {
                var tabContent = new TabContent(tab.Text);
                tab.Controls.Add(tabContent);
                tabContent.Dock = DockStyle.Fill;
                var lst = File.ReadAllLines(savingPath);
                foreach (var item in lst)
                {
                    Person p = new Person(item);
                    tabContent.pnlTo.Controls.Add(p);
                }
            }
        }

        void SearchData(string searchKey = "")
        {
            if (tabControl1.SelectedIndex == -1) return;
            foreach (var item in (tabControl1.SelectedTab.Controls[0] as TabContent).pnlTo.Controls)
            {
                var per = (item as Person);
                if (string.IsNullOrEmpty(searchKey))
                {
                    per.Larg();
                }
                else
                {
                    if (per.txtName.Text.ToLower().Contains(searchKey))
                    {
                        per.Larg();
                        continue;
                    }
                    if (per.txtTo.Text.ToLower().Contains(searchKey))
                    {
                        per.Larg();
                        continue;
                    }
                    if (per.txtAttach1.Text.ToLower().Contains(searchKey))
                    {
                        per.Larg();
                        continue;
                    }
                    if (per.txtAttach2.Text.ToLower().Contains(searchKey))
                    {
                        per.Larg();
                        continue;
                    }
                    per.Small();
                }
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == -1)
            {
                MessageBox.Show("Select tab first");
                return;
            }
            progressBar1.Maximum = (tabControl1.SelectedTab.Controls[0] as TabContent).pnlTo.Controls.Count;
            progressBar1.Value = 0;
            foreach (var item in (tabControl1.SelectedTab.Controls[0] as TabContent).pnlTo.Controls)
            {
                if (item is Person)
                {
                    progressBar1.Value++;
                    var person = item as Person;
                    var data = person.GetData();
                    Utility.sendMail
                        (data.To,
                        data.Attachments.ToArray(),
                        txtSubject.Text,
                        txtBody.Text,
                        txtFrom.Text,
                        txtServer.Text,
                        Convert.ToInt32(txtPort.Text),
                        txtPassword.Text);
                }
            }
            MessageBox.Show("done");
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchData(txtSearch.Text);
        }

        private void btnAddTab_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Did you save other tabs? this operation will load the other tabs again", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (!string.IsNullOrEmpty(txtNewTabName.Text))
                {
                    if (!File.Exists($"{SavingDirectory}\\{txtNewTabName.Text}.txt"))
                    {
                        File.WriteAllText($"{SavingDirectory}\\{txtNewTabName.Text}.txt", "");
                        LoadData();
                    }
                }
                else
                {
                    MessageBox.Show("Please input tab name");
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchData(txtSearch.Text);
        }
    }
}
