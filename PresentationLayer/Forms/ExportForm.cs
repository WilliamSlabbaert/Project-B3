using Import_Export;
using PresentationLayer.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.Forms
{
    public class ExportForm
    {
        private TextBox FilepathInput;
        private Button FileselectButton;
        private Button SubmitButton;

        public ExportForm(TextBox filepath, Button fileselect, Button submit)
        {
            this.FilepathInput = filepath;
            this.FilepathInput.TextChanged += InputChanged;
            this.FileselectButton = fileselect;
            this.FileselectButton.Click += Fileselect;
            this.SubmitButton = submit;
            this.SubmitButton.Click += Submit;
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            try
            {
                if(!File.Exists(this.FilepathInput.Text) || MessageUtil.ShowYesNoMessage("File Exists", "Are you sure you want to override that file?"))
                {
                    Export.export(this.FilepathInput.Text);
                    if (!File.Exists(this.FilepathInput.Text)) throw new Exception("File not exported! Something went wrong.");
                    Process.Start("explorer.exe", this.FilepathInput.Text);
                    Reset();
                }
            }
            catch (Exception ex)
            {
                MessageUtil.ShowAsyncMessage(ex.Message);
            }
        }

        private void InputChanged(object sender, RoutedEventArgs e)
        {
            bool valid = true;
            if (string.IsNullOrWhiteSpace(this.FilepathInput.Text)) valid = false;
            if (!File.Exists(this.FilepathInput.Text)) valid = false;
            if (Path.GetExtension(this.FilepathInput.Text).ToLower() != ".json") valid = false;
            this.SubmitButton.IsEnabled = valid;
        }

        private void Reset()
        {
            this.FilepathInput.Text = "";
            this.SubmitButton.IsEnabled = false;
        }

        private void Fileselect(object sender, RoutedEventArgs e)
        {
            string path = FileUtil.SaveFile("Save Export File", "JSON Import File|*.json");
            if (!string.IsNullOrWhiteSpace(path))
                this.FilepathInput.Text = path;
        }
    }
}
