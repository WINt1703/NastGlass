using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Gdk;
using GLib;
using Gtk;
using Application = Gtk.Application;
using Process = System.Diagnostics.Process;
using UI = Gtk.Builder.ObjectAttribute;
using Window = Gtk.Window;

namespace NastGlass
{
    internal class MainWindow : Window
    {
        #region UIFields

        [UI] private FileChooserButton _selected = null;
        [UI] private Button _getLink = null;
        [UI] private Button _openGit = null;
        [UI] private Button _closeApplication = null;
        [UI] private Button _hideApplication = null;
        [UI] private TextView _messange = null;
        [UI] private ProgressBar _progressBar = null;
        
        #endregion
        
        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetObject("MainWindow").Handle)
        {
            builder.Autoconnect(this);
            
            this._openGit.Clicked += OpenGitClicked;
            this._hideApplication.Clicked += HideApplication;
            this._closeApplication.Clicked += CloseApplication;
            this._getLink.Clicked += GetLinkClicked;
        }

        private void HideApplication(object sender, EventArgs e) => this.Hide();
        
        private void CloseApplication(object sender, EventArgs e) => ((GLib.Application) this.Application).Quit();

        private void OpenGitClicked(object sender, EventArgs e)
        {
            var url = "https://github.com/wint1703";
            
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url); 
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
        }
        
        private void GetLinkClicked(object sender, EventArgs e)
        {
            var filePath = _selected.File?.Basename + _selected.File?.Path;
            
            if (string.IsNullOrEmpty(filePath))
            {
                MessageDialog md = new MessageDialog(this, DialogFlags.Modal, MessageType.Error, ButtonsType.Close, "Select a file"){Decorated = false};
                md.Run();
                md.Dispose();
            }
            else
            {
                var file = new System.IO.FileInfo(filePath);
            }
        }
    }
}
