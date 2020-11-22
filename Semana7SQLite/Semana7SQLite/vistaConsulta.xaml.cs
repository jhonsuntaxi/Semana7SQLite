using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using Semana7SQLite.Models;
using System.Collections.ObjectModel;

namespace Semana7SQLite
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vistaConsulta : ContentPage
    {
        private SQLiteAsyncConnection _conn;
        private ObservableCollection<Estudiante> _TblEstudiante;
        public vistaConsulta()
        {
            InitializeComponent();
            _conn = DependencyService.Get<Database>().GetConnection();
            NavigationPage.SetHasBackButton(this, false);
            
        }

        protected async override void OnAppearing()
        {
            var resultRegistro = await _conn.Table<Estudiante>().ToArrayAsync();
            _TblEstudiante = new ObservableCollection<Estudiante>(resultRegistro);
            lstUsuarios.ItemsSource = _TblEstudiante;
            base.OnAppearing();
        }

        private void lstUsuarios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Estudiante)e.SelectedItem;
            var item = obj.Id.ToString();
            int Id = Convert.ToInt32(item);
            try
            {
                Navigation.PushAsync(new vistaElemento(Id));
            }
            catch (Exception ex)
            {

                DisplayAlert("Alerta", ex.Message, "Ok");
            }

        }
    }
}