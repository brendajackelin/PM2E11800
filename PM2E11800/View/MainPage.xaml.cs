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


namespace PM2E11800
{
    public partial class MainPage : ContentPage
    {
        public bool takedfoto = false;
        public byte[] imgbyte;
        public Image image = new Image();
        string base64ball = "";

        public MainPage()
        {
            InitializeComponent();
            Ubicacion();
        }

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

        }

        //Validar ubicacion
        private async void Ubicacion()
        {
            //Validar si inicia el pugin
            if (!CrossGeolocator.IsSupported)
            {
                await DisplayAlert("Error", "Ha ocurrido un error al cargar el plugin", "OK");
                return;
            }

            //Valida si el GPS está activo
            if (CrossGeolocator.Current.IsGeolocationEnabled)
            {
                CrossGeolocator.Current.PositionChanged += Current_PositionChanged;

                await CrossGeolocator.Current.StartListeningAsync(new TimeSpan(0, 0, 1), 0.5);

            }
            else
            {
                await DisplayAlert("Error", "El GPS no está activo en este dispositivo", "OK");
            }

        }

        //Obtener la ubicación
        private void Current_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            if (!CrossGeolocator.Current.IsListening)
            {

                return;
            }
            var position = CrossGeolocator.Current.GetPositionAsync();

            //Mostrar latitud y longitud en las cajas de texto
            txtlatitud.Text = position.Result.Latitude.ToString();
            txtlongitud.Text = position.Result.Longitude.ToString();
        }

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
                        imagen = base64ball

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
            }else{
                await DisplayAlert("Error", "No se pudo guardar la ubicacion", "Ok");
            }
        }

        //Validación de campos
        private async Task Validar()
        {
            if (String.IsNullOrWhiteSpace(txtlatitud.Text) ||
                String.IsNullOrWhiteSpace(txtlongitud.Text) ||
                String.IsNullOrWhiteSpace(txtdescripcion.Text) ||
                takedfoto == false)
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



    }
}
