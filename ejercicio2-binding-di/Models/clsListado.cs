using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;


namespace ejercicio2_binding_di.Models
{
    public class clsListado
    {
        //Ruta hacia la Api Rest
        private Uri miUrl = new Uri("http://apirestdaviduwp.azurewebsites.net/api/persona");
        public async Task<ObservableCollection<clsPersona>> getListado()
        {
            //Lo usabamos cuando no teniamos conexion a la BD
            //ObservableCollection<clsPersona> lista = new ObservableCollection<clsPersona>();

            //lista.Add(new clsPersona(1, "Alvaro", "Tellez", DateTime.Now, "Moron", "656456782"));
            //lista.Add(new clsPersona(2, "David", "Benitez", DateTime.Now, "Dos Hermanas", "656456782"));
            //lista.Add(new clsPersona(3, "Merecedes", "Requena", DateTime.Now, "Sevilla", "656456782"));
            //lista.Add(new clsPersona(4, "Jose Antonio", "Ruiz", DateTime.Now, "Carmona", "656456782"));
            //lista.Add(new clsPersona(5, "Daniel", "Leal", DateTime.Now, "Mairena", "656456782"));
            //lista.Add(new clsPersona(6, "Migue", "Barrera", DateTime.Now, "Sevilla", "656456782"));
            //lista.Add(new clsPersona(7, "Carlos", "Prieto", DateTime.Now, "Dos Hermanas", "656456782"));
            //lista.Add(new clsPersona(8, "Adri", "Pol", DateTime.Now, "Moron", "656456782"));

            
            //Lo usamos para la api rest
            //Creacion del cliente 
            HttpClient client = new HttpClient();

            ObservableCollection<clsPersona> lista = new ObservableCollection<clsPersona>();
            try
            {
                //Al hacer el await hay que hacer que el metodo sea Async
                string respuesta = await client.GetStringAsync(miUrl);
                client.Dispose();

                //Convertimos el json para pasarlo luego a la lista
                lista = JsonConvert.DeserializeObject<ObservableCollection<clsPersona>>(respuesta);
            }
            catch (Exception e)
            {
                throw e;
            }
            return lista;
        }
    }
}
