using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Semana7SQLite.Models;
using System.Data;

namespace Semana7SQLite
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaLogin : ContentPage
    {
        private SQLiteAsyncConnection _conn;
        public vistaLogin()
        {
            InitializeComponent();
            _conn = DependencyService.Get<Database>().GetConnection();
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                var documentsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "uisrael.db3");
                var db = new SQLiteConnection(documentsPath);

                db.CreateTable<Estudiante>();
                IEnumerable<Estudiante> resultado = SELECT_WHERE(db, txtUsuario.Text, txtPasswd.Text);
                if (resultado.Count() > 0)
                {
                    await Navigation.PushAsync(new vistaConsulta());
                }
                else
                {
                    await DisplayAlert("Alerta", "Verifique su usuario o Contraseña","Ok");
                }
            }
            catch (Exception ex)
            {

                await DisplayAlert("Alerta", ex.Message, "Ok");
            }
        }

        private async void btnRegistro_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new vistaRegistro());
         }

        public static IEnumerable<Estudiante> SELECT_WHERE(SQLiteConnection db, string usuario, string contra)
        {
            return db.Query<Estudiante>("SELECT * FROM Estudiante WHERE Usuario = ? AND Password = ?", usuario, contra);
        }


    }
} 