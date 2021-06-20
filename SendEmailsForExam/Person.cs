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
    public partial class Person : UserControl
    {
        public Person(string line)
        {
            InitializeComponent();
            LoadData(line);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtAttach1.Text = attach();
        }

        string attach()
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                return op.FileName;
            }
            return "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtAttach2.Text = attach();
        }

        public override string ToString()
        {
            return txtTo.Text + "?" + txtAttach1.Text + "?" + txtAttach2.Text + "?" + txtName.Text;
        }

        void LoadData(string line)
        {
            if(string.IsNullOrEmpty(line))
            {
                return;
            }
            var split = line.Split('?');
            txtTo.Text = split[0];
            txtAttach1.Text = split[1];
            txtAttach2.Text = split[2];
            txtName.Text = split[3];
        }

        private void Person_Load(object sender, EventArgs e)
        {

        }

        public RecipiantData GetData()
        {
            var result = new RecipiantData();
            result.Attachments = new List<string>();
            result.Name = txtName.Text;
            result.To = txtTo.Text.Trim();
            if(!string.IsNullOrEmpty(txtAttach1.Text) && System.IO.File.Exists(txtAttach1.Text))
            {
                result.Attachments.Add(txtAttach1.Text);
            }
            if (!string.IsNullOrEmpty(txtAttach2.Text) && System.IO.File.Exists(txtAttach2.Text))
            {
                result.Attachments.Add(txtAttach2.Text);
            }
            return result;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var mnf = Application.OpenForms["MainFrom"] as MainFrom;
            var data = this.GetData();
            Utility.sendMail
                       (data.To,
                       data.Attachments.ToArray(),
                       mnf.txtSubject.Text,
                       mnf.txtBody.Text,
                       mnf.txtFrom.Text,
                       mnf.txtServer.Text,
                       Convert.ToInt32(mnf.txtPort.Text),
                       mnf.txtPassword.Text);
            MessageBox.Show("done");
        }
    }
}
