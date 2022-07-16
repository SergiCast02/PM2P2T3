using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.AudioRecorder;
using System.IO;
using Xamarin.Essentials;
//using Android.Media;

namespace PM2P2T3
{
    public partial class MainPage : ContentPage
    {
        private readonly AudioRecorderService audioRecorderService = new AudioRecorderService();
        private readonly AudioPlayer audioPlayer = new AudioPlayer();
        public string AudioPath, fileName;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnGrabar_Clicked(object sender, EventArgs e)
        {
            var status = await Permissions.RequestAsync<Permissions.Microphone>();
            var status2 = await Permissions.RequestAsync<Permissions.StorageRead>();
            var status3 = await Permissions.RequestAsync<Permissions.StorageWrite>();
            if (status != PermissionStatus.Granted & status2 != PermissionStatus.Granted & status3 != PermissionStatus.Granted)
            {
                return; // si no tiene los permisos no avanza
            }

            if (string.IsNullOrEmpty(txtdescripcion.Text))
            {
                await DisplayAlert("Aviso", "Ingrese una descripción antes de comenzar a grabar", "OK");
                return;
            }

            imgmic.Source = "grabandovoz.png";

            if (audioRecorderService.IsRecording)
            {
                await audioRecorderService.StopRecording();
                audioPlayer.Play(audioRecorderService.GetAudioFilePath());
            }
            else
            {
                btnGrabar.IsEnabled = false;
                btnDetener.IsEnabled = true;
                await audioRecorderService.StartRecording();
            }

        }




        private async void btnDetener_Clicked(object sender, EventArgs e)
        {
            imgmic.Source = "grabarvoz.png";


            if (audioRecorderService.IsRecording)
            {
                await audioRecorderService.StopRecording();
                saveAudio();

                if (await DisplayAlert("Audio", "¿Desea reproducir el audio?", "SI", "NO"))
                {
                    audioPlayer.Play(audioRecorderService.GetAudioFilePath());
                }
                btnGrabar.IsEnabled = true;
                btnDetener.IsEnabled = false;

            }
            else
            {
                await audioRecorderService.StartRecording();
            }

        }


        private async void saveAudio()
        {
            if (audioRecorderService.FilePath != null)
            {

                var stream = audioRecorderService.GetAudioFileStream();

                fileName = Path.Combine(FileSystem.CacheDirectory, DateTime.Now.ToString("ddMMyyyymmss") + "_VoiceNote.wav");

                using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    stream.CopyTo(fileStream);
                }

                AudioPath = fileName;

            }



            Models.Audio audio = new Models.Audio();

            audio.Url = AudioPath;
            audio.Descripcion = txtdescripcion.Text;
            audio.Date = DateTime.Now;

            var resultado = await App.BaseDatos.GrabarAudio(audio);

            if (resultado == 1)
            {
                //await DisplayAlert("", "El audio " + audio.Descripcion + " guardado en la ruta " + audio.Url, "ok");
                txtdescripcion.Text = "";
            }
            else
            {
                await DisplayAlert("Advertencia", "Audio No guardado", "ok");
            }
        }

        private async void btnlistarAudio_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.ListaAudio());
        }
    }
}
