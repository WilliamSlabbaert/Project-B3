using BusinessLayer;
using DataLayer;
using PresentationLayer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.Forms
{
    public class AuthorAddForm
    {
        private TextBox FirstnameInput;
        private TextBox LastnameInput;
        private Button SubmitButton;

        public AuthorAddForm(TextBox firstname, TextBox lastname, Button submit)
        {
            this.FirstnameInput = firstname;
            this.FirstnameInput.TextChanged += InputChanged;
            this.LastnameInput = lastname;
            this.LastnameInput.TextChanged += InputChanged;
            this.SubmitButton = submit;
            this.SubmitButton.Click += Submit;
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            try
            {
                AuthorManager am = new AuthorManager(new UnitOfWork());
                am.Add(new Author(this.FirstnameInput.Text, this.LastnameInput.Text));
                MessageUtil.ShowAsyncMessage("Author has been added");
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
            if (string.IsNullOrWhiteSpace(this.FirstnameInput.Text)) valid = false;
            if (string.IsNullOrWhiteSpace(this.LastnameInput.Text)) valid = false;
            this.SubmitButton.IsEnabled = valid;
        }

        private void Reset()
        {
            this.FirstnameInput.Text = "";
            this.LastnameInput.Text = "";
            this.SubmitButton.IsEnabled = false;
        }
    }
}
