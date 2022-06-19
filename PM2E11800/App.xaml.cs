using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PM2E11800.Controller;
using System.IO;
using PM2E11800.Models;

namespace PM2E11800
{
    public partial class App : Application
    {
        static Database database;

        public static Database BD
        {
            get
            {

                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PM2E12513.db3"));
                }

                return database;

            }

        }

        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
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
