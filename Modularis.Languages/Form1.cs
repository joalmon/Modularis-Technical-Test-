using Language.Container;
using Language.Interface;
using System;
using System.Windows.Forms;

namespace Modularis.Language
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var messages = MessageContainer.Instance.Messages;

            LanguageComboBox.DisplayMember = "Description";
            LanguageComboBox.DataSource = messages;

        }

        private void LenguajesCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Text = (LanguageComboBox.SelectedItem as IMessage).Message;
        }
    }
}