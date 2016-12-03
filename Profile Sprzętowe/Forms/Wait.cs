using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Profile_Sprzętowe.Forms
{
    public partial class Wait : Form
    {
        static Wait instance;
        public static Wait Instance { get { return instance; } }

        public Wait()
        {
            InitializeComponent();
        }

        private void Wait_Load(object sender, EventArgs e)
        {
            instance = this;
        }

        public void updateProgressBar(int percent)
        {
            progressBar1.Value = percent;
            progressBar1.Update();
            progressBar1.Refresh();
            progressBar1.Invalidate();
        }
    }
}
