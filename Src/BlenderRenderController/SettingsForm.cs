﻿// For Mono compatible Unix builds, uncomment
// the next line or compile with /d:UNIX
//#define UNIX
#if !WINDOWS && !UNIX
#define WINDOWS
#elif UNIX
#undef WINDOWS
#endif

using NLog;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;


namespace BlenderRenderController
{
    public partial class SettingsForm : Form
    {
        private AppSettings _appSettings;
        private OpenFileDialog _changePathDialog;

        public SettingsForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
            _appSettings = AppSettings.Current;
        }

        private void onFormLoad(object sender, EventArgs e)
        {
            appSettingsBindingSource.DataSource = _appSettings;

            if (!File.Exists(_appSettings.BlenderProgram))
            {
                blenderPathTextBox.Text = string.Empty;
            }
            if (!File.Exists(_appSettings.FFmpegProgram))
            {
                ffmpegPathTextBox.Text = string.Empty;
            }

#if UNIX
            ffmpegPathTextBox.BackColor =
            blenderPathTextBox.BackColor = System.Drawing.Color.White;
#endif
        }

        private void blenderChangePathButton_Click(object sender, EventArgs e)
        {
            _changePathDialog = new OpenFileDialog()
            {
                Filter = "Blender|" + _appSettings.BlenderExeName,
                Title = "Find " + _appSettings.BlenderExeName,
                InitialDirectory = blenderPathTextBox.Text.Trim(),
                CheckFileExists = true
            };
            var result = _changePathDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                blenderPathTextBox.Text = _changePathDialog.FileName;
            }
        }

        private void ffmpegChangePathButton_Click(object sender, EventArgs e)
        {
            _changePathDialog = new OpenFileDialog()
            {
                Filter = "FFmpeg|" + _appSettings.FFmpegExeName,
                Title = "Find " + _appSettings.FFmpegExeName,
                InitialDirectory = ffmpegPathTextBox.Text.Trim(),
                CheckFileExists = true
            };
            var result = _changePathDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                ffmpegPathTextBox.Text = _changePathDialog.FileName;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {

            Close();
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!_appSettings.CheckCorrectConfig())
            {
                this.DialogResult = DialogResult.Abort;
                //Application.Exit();
            }

        }

        private void ffmpegDownloadLabel_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.ffmpeg.org/download.html");
        }

    }
}
