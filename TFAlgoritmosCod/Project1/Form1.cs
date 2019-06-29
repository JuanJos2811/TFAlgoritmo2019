using Project1.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var context = new TFAlgoritmosEntities();
            var algo = new Algoritmo();
        }
        private void btnCalcular_Click(object sender, EventArgs e)
        {
            Algoritmo algo = new Algoritmo();
            var context = new TFAlgoritmosEntities();
            List<Origen_Destino> cont = new List<Origen_Destino>();
            cont = context.Origen_Destino.ToList();
            double[,] matriz = algo.CrearMatriz();
            int origen = Convert.ToInt32(txtOrigen.Text);
            int countVertices = cont.Count();
            algo.Dijkstra(matriz, origen, countVertices);

            for(int i=0;i<algo.Lista_Padre.Count();i++)
            {
                listView1.Items.Add(Convert.ToString(i));
            }

            for(int j = 0; j< algo.Lista_Distancias.Count(); j++)
            {
                listView2.Items.Add(Convert.ToString(j));
            }

            MessageBox.Show("Listo");
        }
    }
}
