using System;
using System.IO;
using System.Reflection;
using Gtk;

namespace NastGlass
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var app = new Application("org.NastGlass.NastGlass", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);
            
            var win = new MainWindow(){Application = app};
            
            string fileStyle = $"{Directory.GetCurrentDirectory()}/StylesWindows.css";
            LoadCssStyles(fileStyle, win);
            
            var iconPath = $"{Directory.GetCurrentDirectory()}/Icon.ico";
            win.SetIconFromFile(iconPath);
            app.AddWindow(win);
            win.Show();

            ((GLib.Application) app).Run();
        }

        private static void LoadCssStyles(string fileStyle, Window win)
        {
            if (File.Exists(fileStyle))
            {
                CssProvider provider = new CssProvider();
                provider.LoadFromPath(fileStyle);
                StyleContext.AddProviderForScreen(win.Screen, provider, 800);
            }
        }
    }
}