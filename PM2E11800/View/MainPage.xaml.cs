using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Plugin.Media;
using Xamarin.Forms.Xaml;
using System.IO;
using Plugin.Geolocator;
using PM2E11800.View;

namespace PM2E11800
{
    public partial class MainPage : ContentPage
    {
        public bool takedfoto = false;
        //string base64ball = "";

        Plugin.Media.Abstractions.MediaFile FileFoto = null;

        public MainPage()
        {
            InitializeComponent();
            GetLocation();
        }

        //Obtener ubicación
        public async void GetLocation()
        {
            Location Location = await Geolocation.GetLastKnownLocationAsync();

            if (Location == null)
            {
                Location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                }); ;
            }

            //Mostrar en las cajas de texto
            txtlatitud.Text = Location.Latitude.ToString();
            txtlongitud.Text = Location.Longitude.ToString();


        }

        /*
        //Metodo boton tomar foto
        private async void TomarFoto(object sender, EventArgs e)
        {
            var tomarfoto = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "miApp",
                Name = "Image.jpg"

            });

            if (tomarfoto != null)
            {
                Foto.Source = ImageSource.FromStream(() => {
                    takedfoto = true;
                    return tomarfoto.GetStream(); });
            }

            Byte[] imagenByte = null;

            using (var stream = new MemoryStream())
            {
                tomarfoto.GetStream().CopyTo(stream);
                tomarfoto.Dispose();
                imagenByte = stream.ToArray();

                base64ball = Convert.ToBase64String(imagenByte);

            }

        }*/

        //Guardar los datos en la base de datos de SQLite
        private async void Guardar(object sender, EventArgs e)
        {
            if (Validar().IsCompleted)
            {
                try
                {
                    var sites = new Models.sitios
                    {
                        latitud = Convert.ToDouble(this.txtlatitud.Text),
                        longitud = Convert.ToDouble(this.txtlongitud.Text),
                        descripcion = this.txtdescripcion.Text,
                        //imagen = base64ball
                        imagen = ConvertImageToByteArray()
                    };

                    var resultado = await App.BD.GrabarUbicacion(sites);


                    if (resultado == 1)
                    {
                        ClearScreen();
                        await DisplayAlert("Mensaje", "Datos ingresados correctamente", "OK");
                    }
                    else
                    {
                        await DisplayAlert("Mensaje", "Error, No se logró guardar la información", "OK");

                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Mensaje", ex.Message.ToString(), "OK");

                }
            }
        }

        //Validación de campos
        private async Task Validar()
        {
            if (String.IsNullOrWhiteSpace(txtlatitud.Text) ||
                String.IsNullOrWhiteSpace(txtlongitud.Text) ||
                String.IsNullOrWhiteSpace(txtdescripcion.Text) ||
                //takedfoto == false
                FileFoto == null)
            {
                await this.DisplayAlert("Advertencia", "Todos los campos son obligatorios", "OK");
            }
        }

        //Limpiar pantalla
        private void ClearScreen()
        {
            // this.txtlatitud.Text = String.Empty;
            //this.txtlongitud.Text = String.Empty;
            this.txtdescripcion.Text = String.Empty;
            Foto.Source = ImageSource.FromFile("imagen.png");
            takedfoto = false;
        }

        //Salir de la App
        private void salir(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        //Ver Lista de sitios
        private async void lista(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Sitios());
        }

        private Byte[] ConvertImageToByteArray()
        {
            if (FileFoto != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    Stream stream = FileFoto.GetStream();
                    stream.CopyTo(memory);
                    return memory.ToArray();
                }
            }
            return null;
        }

        private async void TomarFoto(object sender, EventArgs e)
        {
            FileFoto = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "MisFotos",
                Name = "test.jpg",
                SaveToAlbum = true
            });

            await DisplayAlert("path directorio", FileFoto.Path, "OK");

            if (FileFoto != null)
            {
                Foto.Source = ImageSource.FromStream(() =>
                {
                    return FileFoto.GetStream();
                });
            }
        }
    }
}
