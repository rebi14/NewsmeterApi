using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class EntityFrameWorkForm : Form
    {
        NewsEntities4 context;
        private List<News> newsList = null;
        public EntityFrameWorkForm()
        {
            InitializeComponent();
            // form açılınca news tablosunu datagride aktarmak için;
            context = new NewsEntities4();
            var query = from c in context.News select c;
            newsList = query.ToList();
            dataGridView1.DataSource = newsList;

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                int id = Convert.ToInt32(row.Cells["Id"].Value.ToString());
                News news = newsList.Where(x => x.Id == id).FirstOrDefault();
                context.News.Remove(news);
                newsList.Remove(news);
            }
            context.SaveChanges();
            dataGridView1.Refresh();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
