using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendEmailsForExam
{
    public partial class MainFrom : Form
    {
        public MainFrom()
        {
            InitializeComponent();
        }

        private void MainFrom_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Person p = new Person("");
            pnlTo.Controls.Add(p);
        }
        string savingPath = Application.StartupPath + "\\lst.txt";

        private void BtnSaveData_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.From = txtFrom.Text;
            Properties.Settings.Default.Subject = txtSubject.Text;
            Properties.Settings.Default.Body = txtBody.Text;
            Properties.Settings.Default.Server = txtServer.Text;
            Properties.Settings.Default.Port = txtPort.Text;
            Properties.Settings.Default.Password = txtPassword.Text;
            Properties.Settings.Default.Save();

            List<string> lst = new List<string>();
            foreach (var item in pnlTo.Controls)
            {
                if (item is Person)
                {
                    var person = item as Person;
                    lst.Add(person.ToString());
                }
            }

            if (lst.Count > 0)
            {
                if (System.IO.File.Exists(savingPath))
                {
                    System.IO.File.Delete(savingPath);
                }
                System.IO.File.WriteAllLines(savingPath, lst);
            }
            MessageBox.Show("done");
        }

        void LoadData()
        {
            txtFrom.Text = Properties.Settings.Default.From;
            txtBody.Text = Properties.Settings.Default.Body;
            txtPassword.Text = Properties.Settings.Default.Password;
            txtServer.Text = Properties.Settings.Default.Server;
            txtSubject.Text = Properties.Settings.Default.Subject;
            txtPort.Text = Properties.Settings.Default.Port;

            if (System.IO.File.Exists(savingPath))
            {
                var lst = System.IO.File.ReadAllLines(savingPath);
                foreach (var item in lst)
                {
                    Person p = new Person(item);
                    pnlTo.Controls.Add(p);
                }
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            progressBar1.Maximum = pnlTo.Controls.Count;
            progressBar1.Value = 0;
            foreach (var item in pnlTo.Controls)
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
    }
}
