using Plugin.AudioRecorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2P2T3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaAudio : ContentPage
    {
        private readonly AudioPlayer audioPlayer = new AudioPlayer();
        Models.Audio audio;


        public ListaAudio()
        {
            InitializeComponent();
        }

        private void listaAudios_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            audio = (Models.Audio)e.Item;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listaAudios.ItemsSource = await App.BaseDatos.ObtenerListaAudios();
        }

        private async void btndelete_Invoked(object sender, EventArgs e)
        {
            if (audio != null)
            {
                if (await DisplayAlert("Eliminar audio", "¿Esta seguro de eliminar el audio: -" + audio.Descripcion + " -?", "SI", "NO"))
                {
                    await App.BaseDatos.EliminarAudio(audio);
                    await Navigation.PopAsync();
                }
            }
            else
            {
                await DisplayAlert("Aviso", "Haga clic sobre el elemento que desea reproducir o eliminar", "OK");
            }
            


        }

        private async void btnplay_Invoked(object sender, EventArgs e)
        {
            if (audio != null)
            {
                var ruta = await App.BaseDatos.ObtenerAudio(audio.Id);
                audioPlayer.Play(ruta.Url);
            }
            else
            {
                await DisplayAlert("Aviso", "Haga clic sobre el elemento que desea reproducir o eliminar", "OK");
            }

        }


    }
}