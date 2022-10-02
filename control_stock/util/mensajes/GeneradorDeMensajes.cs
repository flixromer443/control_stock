using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_stock.util
{
    internal class GeneradorDeMensajes
    {
        public void generarMensaje(string mensaje, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon)
        {
            MessageBox.Show(mensaje, "Operaciones", messageBoxButtons, messageBoxIcon);
        }

        public DialogResult generarMensaje(string mensaje, MessageBoxIcon messageBoxIcon)
        {
            DialogResult response = MessageBox.Show(mensaje, "Operaciones", MessageBoxButtons.OKCancel, messageBoxIcon);
            return response;
        }
        public void generarError(string mensaje)
        {
            MessageBox.Show(mensaje, "Operaciones", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
