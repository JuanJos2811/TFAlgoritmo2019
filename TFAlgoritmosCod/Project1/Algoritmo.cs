using Project1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public  class Algoritmo
    {
        public List<int> Lista_Padre = new List<int>();
        public List<double> Lista_Distancias = new List<double>();
        
        public List<int> LPadres()
        {
            return Lista_Padre;
        }
        public void Add_Padres(int _i)
        {
            Lista_Padre.Add(_i);
        }

        public void Add_Distancia(double _d)
        {
            Lista_Distancias.Add(_d);
        }
        public List<double> LDistancias()
        {
            return Lista_Distancias;
        }

        private  double DistanciaMinima(double[] _distancia, bool[] _corto, int _count)
        {
            var context = new TFAlgoritmosEntities();
            double min = double.MaxValue;
            double minIndex = 0.0;

            for (int i = 0; i<_count; ++i)
            {
                if (_corto[i] == false && _distancia[i] <= min)
                {
                    min = _distancia[i];
                    minIndex = i;
                }
            }

            return minIndex;
        }
        
       
        public  void Dijkstra(double[,] _matriz, int _origen, int _count)
        {
            double[] distancia = new double[_count];
            bool[] corto = new bool[_count];
            for (int i = 0; i < _count; ++i)
            {
                distancia[i] = 0;
                corto[i] = false;
            }

            distancia[_origen] = 99999999.9999999;

            for (int count = 0; count < _count; ++count)
            {
                double u = DistanciaMinima(distancia, corto, _count);
                corto[Convert.ToInt32(u)] = true;

                for (int v = 0; v < _count; ++v) {
                    if (!corto[v] 
                                  && distancia[Convert.ToInt32(u)] + _matriz[Convert.ToInt32(u), v] > distancia[v])
                    {
                        distancia[v] = distancia[Convert.ToInt32(u)] + _matriz[Convert.ToInt32(u), v];
                        //
                        Add_Padres(count);
                          Add_Distancia(distancia[v]);
                        //
                        
                     }
                  
                }               
            }
        }

        public  double[,] CrearMatriz()
        {           
            List<Origen_Destino> orde = new List<Origen_Destino>();
            var context = new TFAlgoritmosEntities();
            orde = context.Origen_Destino.ToList();

            int cantidad = orde.Count();
            double[,] matriz = new double[cantidad, cantidad];
            double min = 99999999.9999999;
            for (int i = 0; i < cantidad; i++)
            {
                for(int j = 0; j < cantidad; j++)
                {
                    matriz[i,j] = min;
                }
            }
            int indice = 0;
            
            for (int i = 0; i < cantidad; i++)
            {
                for (int j = indice; j < cantidad; j++)
                {
                    if (i==orde[i].OrigenId && j==orde[j].DestinoId)
                    {
                        matriz[i, j] = orde[j].Distancia;
                    }
                    indice++;
                }
                
            }
            return matriz;
        }
    }
}
