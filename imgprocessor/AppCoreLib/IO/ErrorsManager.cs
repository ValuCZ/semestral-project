using System.Linq;
using System.Windows.Forms;

namespace imgprocessor.AppCoreLib.IO
{
    public static class ErrorsManagerG
    {
        public static void ErrThrow(string message, string headings)
        {
            int formErrCount = Application.OpenForms.OfType<FormNoImage>().Count();
            if (formErrCount >= 1) return;
            if (message == null) { var _ = new FormNoImage(); _.Show(); }
            else { var _ = new FormNoImage(message, headings); _.Show(); }
        }
    }
}