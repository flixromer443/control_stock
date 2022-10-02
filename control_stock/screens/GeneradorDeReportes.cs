using control_stock.DTO;
using control_stock.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace control_stock.screens
{
    public partial class GeneradorDeReportes : Form
    {
        private ProductosUtil productosUtil = new ProductosUtil();
        private List<ProductoDTO> productos = new List<ProductoDTO>();
        private GeneradorDeMensajes generadorDeMensajes = new GeneradorDeMensajes();

        private string categoriaNombre;
        private int indice = 0;
        private int pagina = 1;

        public string CategoriaNombre { get => categoriaNombre; set => categoriaNombre = value; }
        public List<ProductoDTO> Productos { get => productos; set => productos = value; }

        public GeneradorDeReportes()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


        private void button6_Click(object sender, EventArgs e)
        {
            indice = 0;
            pagina = 1;
            
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
            indice = 0;
            pagina = 1;
            printDialog2.Document = printDocument2;
            if(printDialog2.ShowDialog() == DialogResult.OK)
            {
                printDocument2.Print();
            }

        }

        private void imprimirStock(object sender, PrintPageEventArgs e)
        {
            Font tituloFont = new Font("arial", 25,FontStyle.Bold);
            Font fechaFont1 = new Font("arial", 10, FontStyle.Bold);
            Font fechaFont2 = new Font("arial", 10);
            Font encabezadoFont = new Font("arial", 12, FontStyle.Bold);

            Pen pen = new Pen(Color.Black,5);

            int width = 350;
            int height = 350;
            //ENCABEZADO
            if (pagina == 1)
            {
                string[] fechaYhora = DateTime.Now.ToString().Split(" ");
                e.Graphics.DrawString("Fecha: ", fechaFont1, Brushes.Black, new RectangleF(60, 40, width, height));
                e.Graphics.DrawString(fechaYhora[0], fechaFont2, Brushes.Black, new RectangleF(110, 40, width, height));

                e.Graphics.DrawString("Hora: ", fechaFont1, Brushes.Black, new RectangleF(60, 60, width, height));
                e.Graphics.DrawString(fechaYhora[1], fechaFont2, Brushes.Black, new RectangleF(110, 60, width, height));

                e.Graphics.DrawString("Categoria: ", fechaFont1, Brushes.Black, new RectangleF(60, 80, width, height));
                e.Graphics.DrawString(categoriaNombre, fechaFont2, Brushes.Black, new RectangleF(140, 80, width, height));

                e.Graphics.DrawString("Stock", tituloFont, Brushes.Black, new RectangleF(355, 95, width, height));
            }
            e.Graphics.DrawString("ID", encabezadoFont, Brushes.Black, new RectangleF(60, 140, width, height));
            e.Graphics.DrawString("DESCRIPCION", encabezadoFont, Brushes.Black, new RectangleF(150, 140, width, height));
            e.Graphics.DrawString("P.VENTA", encabezadoFont, Brushes.Black, new RectangleF(550, 140, width, height));
            e.Graphics.DrawString("STOCK", encabezadoFont, Brushes.Black, new RectangleF(680, 140, width, height));
            //ENCABEZADO


            e.Graphics.DrawLine(pen, 60, 160, 760, 160);

            int[] ejesYLV = { 160, 200 };
            //AMBOS EJES AUMENTAN DE 50 EN 50
            while(indice < 32)
            {

                ProductoDTO producto = productos[1];

                //ITEMS = +20 ejesYLV
                e.Graphics.DrawLine(pen, 60, ejesYLV[0], 60, ejesYLV[1]);
                
                e.Graphics.DrawString(producto.Id.ToString(), encabezadoFont, Brushes.Black, new RectangleF(70, ejesYLV[0] + 10, width, height));
                e.Graphics.DrawLine(pen, 140, ejesYLV[0], 140, ejesYLV[1]);


                string descripcion = (producto.Descripcion.Length > 35) ? productosUtil.reducirCaracteresProducto(producto.Descripcion)
                                                                        : producto.Descripcion;
                e.Graphics.DrawString(descripcion, encabezadoFont, Brushes.Black, new RectangleF(150, ejesYLV[0] + 10, width, height));
                e.Graphics.DrawLine(pen, 540, ejesYLV[0], 540, ejesYLV[1]);
                
                e.Graphics.DrawString("$" + producto.PrecioVenta, encabezadoFont, Brushes.Black, new RectangleF(550, ejesYLV[0] + 10, width, height));
                e.Graphics.DrawLine(pen, 670, ejesYLV[0], 670, ejesYLV[1]);
                
                e.Graphics.DrawString(producto.Stock.ToString(), encabezadoFont, Brushes.Black, new RectangleF(680, ejesYLV[0] + 10, width, height));
                e.Graphics.DrawLine(pen, 760, ejesYLV[0], 760, ejesYLV[1]);
                
                //LINEA = +50 ejesYLV
                e.Graphics.DrawLine(pen, 60, ejesYLV[0] + 40, 760, ejesYLV[0] + 40);

                e.Graphics.DrawString("Pagina - " + pagina.ToString(), encabezadoFont, Brushes.Black, new RectangleF(400, 1120, width, height));



                ejesYLV[0] += 40;
                ejesYLV[1] += 40;
                if(indice == 22 || indice == 45 || indice == 68 || indice == 91 || indice == 114 || indice == 137 || indice == 159 || indice == 183 || indice == 205)
                {
                    indice++;
                    pagina++;
                    e.HasMorePages = true;
                    return;
                }
                else
                {
                    indice++;
                    e.HasMorePages = false;
                }
            }
        }

        
    }
}
