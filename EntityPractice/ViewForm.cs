using EntityPractice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityPractice
{
    public partial class ViewForm : Form
    {
        public ViewForm()
        {
            InitializeComponent();
            BindData();
        }
        private void BindData()
        {
            var context=new ContactsModel();
            var list=context.ContactsTable.ToList();
            dataGridView1.DataSource = list;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var context = new ContactsModel();
            var keyWords=textBox1.Text;
            var searchlist = context.ContactsTable.ToList();
            var search = searchlist.Where((x) => x.Id.Contains(keyWords)||x.Name.Contains(keyWords)||x.Category.Contains(keyWords)).ToList();
            dataGridView1.DataSource=search;
        }
    }
}
