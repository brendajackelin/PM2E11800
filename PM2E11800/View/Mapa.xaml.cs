using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Essentials;
using Pin = Xamarin.Forms.GoogleMaps.Pin;


namespace PM2E11800.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Mapa : ContentPage {
        public Mapa()
        {
            InitializeComponent();
        }


        /*public async void MapaPin() {
              Pin pin = new Pin();
             {
                 Type = PinType.Place,
                 Label = "Tokyo SKYTREE",
                 Address = "Sumida-ku, Tokyo, Japan",
                 Position = new Position(35.71d, 139.81d),
                 Rotation = 33.3f,
                 Tag = "id_tokyo",

             };
            map.Pins.Add(pin);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, Distance.FromMeters(5000)));
        }
        */


        
        /*private async void ObtenerImagen(object sender, EventArgs e)
        {
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
        }*/



       /* private async void compartir(object sender, EventArgs e)
        {
            try
            {
                await Share.RequestAsync(new ShareFileRequest() {
                    Title = "Mira este bonito lugar",
                    File = new ShareFile(image)
                }) ;
            }
            catch(Exception e)
            {

            }
        }*/
        
    }
}