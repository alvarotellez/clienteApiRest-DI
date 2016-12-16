using ejercicio2_binding_di.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace ejercicio2_binding_di.ViewModels
{
    public class clsMainPageVM : clsVMBase
    {
        private clsPersona _personaSeleccionada;
        private ObservableCollection<clsPersona> _listado;
        private DelegateCommand _eliminarCommand;
        private DelegateCommand _buscarCommand;
       
        #region constructor
        public clsMainPageVM()
        {
            rellenarLista();
            _eliminarCommand = new DelegateCommand(EliminarCommand_Executed, EliminarCommand_CanExecute);
        }

        #endregion

        //Lo creamos para poder hacer referencia a la task que nos genera el listado y el constructor no puede ser asincrono
        private async void rellenarLista()
        {
            clsListado lista = new clsListado();
            _listado = await lista.getListado();
            NotifyPropertyChanged("listado");
        }

        #region seleccion de la persona
        public clsPersona personaSeleccionada
        {
            get
            {
                return _personaSeleccionada;
            }
            set
            {
                _personaSeleccionada = value;
                _eliminarCommand.RaiseCanExecuteChanged();
                NotifyPropertyChanged("personaSeleccionada");

            }
        }

        private string _textoABuscar
        {
            get { return _textoABuscar;  }
            set { _textoABuscar = value;  NotifyPropertyChanged("textoABuscar"); }
        }

        

        #endregion

        #region listadoPersonas
        public ObservableCollection<clsPersona> listado
        {
            get
            {
                return _listado;
            }
        }
        #endregion

        #region Metodos 
        /// <summary>
        /// Metodo que usamos para eliminar command, a _eliminarCommand le asignamos un nuevo DelegateCommand
        /// </summary>
        public DelegateCommand eliminarCommand
        {
            get
            {
               
                return _eliminarCommand;
            }
        }

        private void EliminarCommand_Executed()
        {
            _listado.Remove(_personaSeleccionada);
        }

        private bool EliminarCommand_CanExecute()
        {
            Boolean esPosibleBorrar = true;

            if(_personaSeleccionada == null)
            {
                esPosibleBorrar = false;
            }

            return esPosibleBorrar;
        }

        public void Borrar(object sender, RoutedEventArgs e)
        {
            _listado.Remove(_personaSeleccionada);
        }
        #endregion

        #region buscar
        /// <summary>
        /// NO FUNCIONA
        /// </summary>
        public DelegateCommand buscarCommand
        {
            get
            {
                _buscarCommand = new DelegateCommand(BuscarCommand_Executed);
                return _buscarCommand;
            }
        }

        private void BuscarCommand_Executed()
        {
            if (!string.IsNullOrEmpty(_textoABuscar))
            {
                var listaFiltrada = from p in _listado where p.Nombre.StartsWith(_textoABuscar) select p;
                _listado = new ObservableCollection<clsPersona>(listaFiltrada);
            }else
            {
                clsListado oListados = new clsListado();
               // _listado = oListados.getListado();
            }
            NotifyPropertyChanged("_listaPersonas");
        }
        #endregion 

    }
}
