using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Semana7SQLite.Models;
using SQLite;
using System.IO;
using System.Collections.ObjectModel;

namespace Semana7SQLite
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaElemento : ContentPage
    {
        public int IdSeleccion;
        private SQLiteAsyncConnection _conn;
        IEnumerable<Estudiante> resultDelete;
        IEnumerable<Estudiante> resultUpdate;
        public vistaElemento(int id)
        {
            _conn = DependencyService.Get<Database>().GetConnection();
            IdSeleccion = id;
            InitializeComponent();

        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(databasePath);
                resultUpdate = Update(db,txtNombre.Text, txtUsuario.Text, txtPasswd.Text, IdSeleccion );
                await DisplayAlert("Alerta", "Se Actualizo correctamente", "Ok");
            }
            catch (Exception ex)
            {

                await DisplayAlert("Alerta", ex.Message, "Ok");
            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(databasePath);
                resultDelete = Delete(db, IdSeleccion);
                await DisplayAlert("Alerta", "Se elimino correctamente", "Ok");
            }
            catch (Exception ex)
            {

                await DisplayAlert("Alerta", ex.Message, "Ok");
            }
        }

        public static IEnumerable<Estudiante> Update(SQLiteConnection db, string nombre, string usuario, string constra, int id)
        {
            return db.Query<Estudiante>("UPDATE Estudiante SET" +
                " Nombre = ?, Usuario = ?, Password = ? WHERE Id = ?", nombre, usuario, constra, id);
        }

        public static IEnumerable<Estudiante> Delete(SQLiteConnection db, int id)
        {
            return db.Query<Estudiante>("DELETE FROM Estudiante WHERE Id = ?", id);
        }
    }
}