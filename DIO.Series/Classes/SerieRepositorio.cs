using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIO.Series.Interfaces;

namespace DIO.Series
{
    public class SerieRepositorio : IRepositorio<Serie>
    {
        public List<Serie> ListaSeries = new List<Serie>();
        public List<Serie> Lista()
        {
            return ListaSeries;
        }

        public Serie RetornaPorId(int id)
        {
            if (ListaSeries.Any(serie => serie.RetornaId() == id))
                return ListaSeries[id];
            else
            {
                return null;
            }
        }

        public void Insere(Serie objeto)
        {
            ListaSeries.Add(objeto);
        }

        public void Excluir(int id)
        {
            if(ListaSeries.Any(serie => serie.RetornaId() == id))
                ListaSeries[id].Excluir();
        }

        public void Atualiza(int id, Serie objeto)
        {
            if (ListaSeries.Any(serie => serie.RetornaId() == id))
                ListaSeries[id] = objeto;
        }

        public int ProximoId()
        {
            return ListaSeries.Count;
        }
    }
}
