using BusinessLayer;
using BusinessLayer.Models;
using DataLayer;
using PresentationLayer.Utils;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.Forms
{
    public class PublisherAddForm
    {
        private TextBox NameInput;
        private Button SubmitButton;

        public PublisherAddForm(TextBox name,  Button submit)
        {
            this.NameInput = name;
            this.NameInput.TextChanged += InputChanged;
            this.SubmitButton = submit;
            this.SubmitButton.Click += Submit;
        }
        
        private void Submit(object sender, RoutedEventArgs e)
        {
            try
            {
                PublisherManager pm = new PublisherManager(new UnitOfWork());
                pm.Add(new Publisher(NameInput.Text));
                MessageUtil.ShowAsyncMessage("Publisher has been added");
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
            if (string.IsNullOrWhiteSpace(this.NameInput.Text)) valid = false;
            this.SubmitButton.IsEnabled = valid;
        }

        private void Reset()
        {
            this.NameInput.Text = "";
            this.SubmitButton.IsEnabled = false;
        }
    }
}
