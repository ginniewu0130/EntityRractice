using EntityPractice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EntityPractice
{
    public partial class AddForm : Form
    {
        public AddForm()
        {
            InitializeComponent();
            addData();
        }
        private void addData()
        {
            var context = new ContactsModel();
            var list = context.ContactsTable.ToList();
            dataGridView1.DataSource = list;
        }
        //新增商品
        private void button1_Click(object sender, EventArgs e)
        {
            ContactsTable data = new ContactsTable()
            {
                Id = textBox1.Text.Trim(),
                Name = textBox2.Text.Trim(),
                Quantity = textBox3.Text.Trim(),
                Price = textBox4.Text.Trim(),
                Category = textBox5.Text.Trim(),
            };
            try
            {
                ContactsModel context = new ContactsModel();
                context.ContactsTable.Add(data);
                context.SaveChanges();
                addData();
                MessageBox.Show("存檔完成");
                ClearTextBoxes();
            }
            catch (Exception ex) {MessageBox.Show($"發生錯誤{ex.ToString()}"); }
            

        }
        private void ClearTextBoxes()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }
        //修改商品
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //搜尋要修改的項目
                var productId = dataGridView1.SelectedRows[1].Cells[0].Value.ToString();
                //從資料庫取得要修改的商品
                using (ContactsModel context = new ContactsModel())
                {
                    var product = context.ContactsTable.FirstOrDefault((x) => x.Id == productId);
                    if (product == null)
                    {
                        MessageBox.Show("找不到該商品");
                        return;
                    }
                    //更新商品資訊
                    product.Name = textBox2.Text;
                    product.Quantity = textBox3.Text;
                    product.Price = textBox4.Text;
                    product.Category =textBox5.Text;
                    context.SaveChanges();
                    //重新載入
                    addData();
                    MessageBox.Show("存檔完成");
                    ClearTextBoxes();
                }    
                
            }
            catch(Exception ex) { MessageBox.Show($"修改失敗{ex.ToString()}"); }
            
        }
        //刪除商品
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //搜尋要刪除的項目
                var productId = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                //從資料庫取得要刪除的商品
                using (ContactsModel context = new ContactsModel())
                {
                    var product = context.ContactsTable.FirstOrDefault((x) => x.Id == productId);
                    if (product == null)
                    {
                        MessageBox.Show("找不到該商品");
                        return;
                    }
                    //從資料庫刪除商品
                        context.ContactsTable.Remove(product);
                        context.SaveChanges();
                        //重新載入
                        addData();
                        MessageBox.Show("刪除完成");
                    ClearTextBoxes();
                }

            }
            catch (Exception ex) { MessageBox.Show($"刪除失敗{ex.ToString()}"); }
        }
    }
}
