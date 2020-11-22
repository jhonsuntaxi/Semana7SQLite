using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Semana7SQLite.Models;

namespace Semana7SQLite
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaRegistro : ContentPage
    {
        private SQLiteAsyncConnection _conn;
        public vistaRegistro()
        {
            InitializeComponent();
            _conn = DependencyService.Get<Database>().GetConnection();
        }

        private void btnRegistrar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var datosRegistro = new Estudiante
                {
                    Nombre = txtNombre.Text,
                    Usuario = txtUsuario.Text,
                    Password = txtPasswd.Text
                };
                _conn.InsertAsync(datosRegistro);

                limpiarDatos();
            }
            catch (Exception ex)
            {

                DisplayAlert("Alerta", ex.Message, "Ok");
            }
            
        }

        void limpiarDatos()
        {
            DisplayAlert("Alerta", "Datos agregados correctaente", "Ok");
            txtNombre.Text = "";
            txtPasswd.Text = "";
            txtUsuario.Text = "";
        }
    }
}