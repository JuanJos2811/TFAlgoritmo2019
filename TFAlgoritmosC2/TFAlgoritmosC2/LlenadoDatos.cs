using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFAlgoritmosC2.Model;


namespace TFAlgoritmosC2
{
   public class LlenadoDatos
    {
       public  LlenadoDatos()
        {
            //_i = OrigenNOPA.Count();
            //_j = OrigenNOPA.Count();
            
            //this.matrizNOPA = _matrizNOPA;
            //matrizNOPA = new double[_i, _j];
        }

        public List<Origen> OrigenNOPA { get; set; }
        public List<Destino> DestinoNOPA { get; set; }
        public List<Origen_Destino> Origen_DestinosNOPA { get; set; }
        public double DistanciaNOPA { get; set; }

        public List<Padron_Origen> OrigenSIPA { get; set; }
        public List<Padron_Destino> DestinoSIPA { get; set; }
        public List<Padron_Origen_Destino> Origen_DestinosSIPA { get; set; }
        public double DistanciaSIPA { get; set; }

        TFAlgoritmosEntities context = new TFAlgoritmosEntities();
        //X Y de Matriz
             //  public int _i;
            //  public int _j;
        public double INF = 696969.99999;
        // Matriz
        public double[,] matrizNOPA;

        // Funcion Calcular distancias + llenado de datos en la BD;

            // CON CANTIDAD
            
            // CON FILTRO PUEBLO

            //CON TODOS
        public void LlenarMatriz()
        {
            //LLenado la matriz
            //Llenado de INF
            int indice = 0; ;
            for (int n = 0; n < OrigenNOPA.Count(); n++) //Horizontal
            {
                for (int m = 0; m < DestinoNOPA.Count(); m++)  //Vertical
                {
                    matrizNOPA[n, m] = INF;
                }
            }



            for (int n = 0; n < OrigenNOPA.Count(); n++) //Horizontal
            {
                for (int m = indice; m < DestinoNOPA.Count(); m++)  //Vertical
                {

                    matrizNOPA[n, m] = Convert.ToDouble(Origen_DestinosNOPA
                                        .Where(x => x.OrigenId == n && x.DestinoId == m)
                                        .Select(x => x.Distancia));                    
                }
            }
        }

        public void LlenarNOPA_BD()
        {
            var context = new TFAlgoritmosEntities();
            //
            OrigenNOPA = context.Origen.ToList();
            DestinoNOPA = context.Destino.ToList();
            Origen_DestinosNOPA = context.Origen_Destino.ToList();



            double p1x, p1y, p2x, p2y;


            int indice = 0;
            //Llenado triangulo inferior.

            for (int i = 0; i < OrigenNOPA.Count(); i++) // Horizontal
            {
                for (int j = indice; j < DestinoNOPA.Count(); j++)
                {

                    double dist;
                    //DB
                    Origen_Destino odnopa = new Origen_Destino();

                    p1x = OrigenNOPA[i].X_LONGITUD;
                    p1y = OrigenNOPA[i].Y_LATITUD;
                    p2x = DestinoNOPA[j].X_LONGITUD;
                    p2y = DestinoNOPA[j].Y_LATITUD;

                    if (i!=j)
                    {
                        dist = Math.Pow(Math.Abs(p2x) - Math.Abs(p1x), 2) + Math.Pow(Math.Abs(p2y) - Math.Abs(p1x), 2);
                    }
                    else
                    {
                        dist = INF;
                    }
                   
                    //Agregar a base de datos.
                    odnopa.Distancia = dist;
                    odnopa.OrigenId = i;
                    odnopa.DestinoId = j;
                    context.Origen_Destino.Add(odnopa);
                    context.SaveChanges();
                    indice++;
                }
            }


        }
    }
}


