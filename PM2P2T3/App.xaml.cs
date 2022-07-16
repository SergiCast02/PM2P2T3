using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PM2P2T3.Models;
using System.IO;

namespace PM2P2T3
{
    public partial class App : Application
    {
        static audioDB basedatos;
        public static audioDB BaseDatos
        {
            get
            {
                if (basedatos == null)
                {
                    basedatos = new audioDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AudiosDB.db3"));
                }
                return basedatos;
            }
        }
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
