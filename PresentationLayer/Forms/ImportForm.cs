using Import_Export;
using PresentationLayer.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.Forms
{
    public class ImportForm
    {
        private TextBox FilepathInput;
        private Button FileselectButton;
        private Button SubmitButton;

        public ImportForm(TextBox filepath, Button fileselect, Button submit)
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
                Import.import(this.FilepathInput.Text);
                MessageUtil.ShowAsyncMessage("File has been imported");
                Reset();
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
            string path = FileUtil.SelectFile(".json", "JSON Import File|*.json");
            if (!string.IsNullOrWhiteSpace(path))
                this.FilepathInput.Text = path;
        }

    }
}
