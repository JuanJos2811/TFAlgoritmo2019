using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TFAlgoritmosC2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }
        LlenadoDatos LD = new LlenadoDatos();

        private void btnLLenarBD_Click(object sender, EventArgs e)
        {
            LD.LlenarNOPA_BD();              
        }
    }
}
