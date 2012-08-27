using System.Windows.Forms;

namespace STDF
{
    public partial class SelectionForm : Form
    {
        /// <summary>
        /// Retrieves a text expression from the user.
        /// </summary>
        /// <example>
        /// string criteria = SelectionForm.GetSelection(this, true);
        /// </example>
        /// <param name="hWnd">Parent window</param>
        /// <param name="Path">Indicates if the desired text expression is for a name or a path.</param>
        /// <returns></returns>
        public static string GetSelection(IWin32Window hWnd, bool Path)
        {
            string Result = "";
            SelectionForm sf = new SelectionForm();

            if (Path)
                sf.Text = "Select Paths Containing";
            else
                sf.Text = "Select Names Containing";

            sf.selectionTextBox.Text = "";

            if (sf.ShowDialog(hWnd) == DialogResult.OK)
            {
                Result = sf.selectionTextBox.Text;
            }

            return Result;
        }

        public SelectionForm()
        {
            InitializeComponent();
        }
    }
}
