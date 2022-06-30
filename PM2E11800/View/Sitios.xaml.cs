using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PM2E11800.Models;
using System.Diagnostics;
using Plugin.Geolocator;

namespace PM2E11800.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Sitios : ContentPage
    {
        public Sitios()
        {
            InitializeComponent();
            BindingContext = new ListViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var listaubicaciones = await App.BD.ObtenerListaUbicaciones();
            ListView.ItemsSource = listaubicaciones;

            

        }

        private async void eliminar(object sender, EventArgs e){       
                var ubicacion = ListView.SelectedItem as Models.sitios;
                if (ubicacion == null)
                {
                    await DisplayAlert("Alerta", "Seleccione un registro", "Ok");
                }
                else
                {
                    
                    bool answer = await DisplayAlert("Alerta", "¿Desea Eliminar el registro seleccionado?", "Yes", "No");
                    Debug.WriteLine("Answer: " + answer);
                    if (answer == true)
                    {

                        var sit = new Models.sitios
                        {
                            id = ubicacion.id,
                            descripcion = ubicacion.descripcion,
                            latitud = ubicacion.latitud,
                            longitud = ubicacion.longitud,
                            imagen = ubicacion.imagen
                        };

                        await App.BD.EliminarUbicacion(ubicacion);
                        await DisplayAlert("Logrado", "Eliminado Exitosamente", "Ok");
                        refresh();

                    }
                    else
                    {

                    }
                }
        }

        

        public async void refresh()
        {
            List<Models.sitios> sit = await App.BD.ObtenerListaUbicaciones();
            ListView.ItemsSource = sit;
        }

        public async void listaSitios_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Models.sitios item = (Models.sitios)e.Item;
            await DisplayAlert("Elemento Tocado " , "descripcion: " + item.descripcion, "Ok");

            //this.txtId.Text = Convert.ToString(item.id);
            this.txtlatitud.Text = Convert.ToString(item.latitud);
            this.txtlongitud.Text = Convert.ToString(item.longitud);
            //this.txtdescripcion.Text = Convert.ToString(item.descripcion);
            


        }


        private async void verMapa(object sender, EventArgs e)
        {
            var ubicacion = ListView.SelectedItem as Models.sitios;
            if (ubicacion == null)
            {
                await DisplayAlert("Alerta", "Seleccione un registro", "Ok");
            }
            else
            {
                bool answer = await DisplayAlert("Alerta", "¿Desea ir a la ubicación indicada?", "Yes", "No");
                Debug.WriteLine("Answer: " + answer);
                if (answer == true)
                {

                    var page = new View.Mapa();
                    await Navigation.PushAsync(page);
                    BindingContext = ubicacion;
                    var Gps = CrossGeolocator.Current;
                }
                else
                {

                }
            }
        }


    }
}