using System;
using System.IO;
using Gtk;

namespace NastGlass
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Init();

            var app = new Application("org.NastGlass.NastGlass", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);
            
            var win = new MainWindow();

            string fileStyle = $"{Directory.GetCurrentDirectory()}/StylesWindows.css";
            
            if (File.Exists(fileStyle))
            {
                CssProvider provider = new CssProvider();
                provider.LoadFromPath(fileStyle);
                StyleContext.AddProviderForScreen(win.Screen, provider, 800);
            }
            
            win.Show();
            Application.Run();
        }
    }
}