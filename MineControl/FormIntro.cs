using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace MineControl
{
    public partial class FormIntro : Form
    {
        public FormIntro()
        {
            InitializeComponent();
        }

        private void FormIntro_Load(object sender, EventArgs e)
        {
            labelIntroTitle.Text += $" v{Application.ProductVersion}";

            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MineControl.Resources.Intro.rtf");
            richTextBoxIntro.LoadFile(stream, RichTextBoxStreamType.RichText);
        }
    }
}
