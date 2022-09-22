using control_stock.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_stock.util
{
    internal class Validador
    {
        public static Boolean validarProducto(ProductoDTO productoDTO)
        {
            if (validarCamposProducto(productoDTO) == true || validarCadenas(productoDTO) == true)
            {
                return true;
            }
            return false;
        }

        public static Boolean validarCamposProducto(ProductoDTO nuevoProducto)
        {
            GenerardorDeMensajes generadorDeMensajes = new GenerardorDeMensajes();
            if (nuevoProducto.Descripcion == null || nuevoProducto.Descripcion.Length == 0)
            {
                generadorDeMensajes.generarError(Mensajes.ERROR_DESCRIPCION);
                return true;
            }
            if (nuevoProducto.PrecioCompra == null || nuevoProducto.PrecioCompra.Length == 0)
            {
                generadorDeMensajes.generarError(Mensajes.ERROR_PRECIO_COMPRA);
                return true;
            }
            if (nuevoProducto.PrecioVenta == null || nuevoProducto.PrecioVenta.Length == 0)
            {
                generadorDeMensajes.generarError(Mensajes.ERROR_PRECIO_VENTA);
                return true;
            }
            if (nuevoProducto.Stock < 0)
            {
                generadorDeMensajes.generarError(Mensajes.ERROR_STOCK);
                return true;
            }
            return false;
        }

        public static Boolean validarCadenas(ProductoDTO productoDTO)
        {
            GenerardorDeMensajes generadorDeMensajes = new GenerardorDeMensajes();

            if (validarCadena(productoDTO.Descripcion) == true)
            {
                if (validarCadena(productoDTO.Descripcion) == true)
                {
                    generadorDeMensajes.generarError(Mensajes.ERROR_CADENAS);
                }
                else
                {
                    generadorDeMensajes.generarError(Mensajes.ERROR_DESCRIPCION);
                }
                return true;
            }
            if (validarCadena(productoDTO.PrecioCompra) == true || validarNumero(productoDTO.PrecioCompra) == true)
            {
                if (validarCadena(productoDTO.PrecioCompra) == true)
                {
                    generadorDeMensajes.generarError(Mensajes.ERROR_CADENAS);
                }
                else
                {
                    generadorDeMensajes.generarError(Mensajes.ERROR_PRECIO_COMPRA);
                }
                return true;

            }
            if (validarCadena(productoDTO.PrecioVenta) == true || validarNumero(productoDTO.PrecioVenta) == true)
            {
                if (validarCadena(productoDTO.PrecioVenta) == true)
                {
                    generadorDeMensajes.generarError(Mensajes.ERROR_CADENAS);
                }
                else
                {
                    generadorDeMensajes.generarError(Mensajes.ERROR_PRECIO_VENTA);
                }
                return true;

            }
            if (validarCadena(productoDTO.Stock.ToString()) == true)
            {
                if (validarCadena(productoDTO.Stock.ToString()) == true)
                {
                    generadorDeMensajes.generarError(Mensajes.ERROR_CADENAS);
                }
                return true;
            }
            return false;

        }

        private static Boolean validarCadena(string cadena)
        {
            string caracteresEspeciales = "=°#$%@!¡¿?<>{}[]&*+_-;,";
            for (int i = 0; i < cadena.Length; i++)
            {
                for (int x = 0; x < caracteresEspeciales.Length; x++)
                {
                    if (cadena[i].ToString() == caracteresEspeciales[x].ToString())
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static Boolean validarNumero(string cadena)
        {
            string numeros = "0123456789";
            for (int i = 0; i < cadena.Length; i++)
            {
                Boolean found = false;
                for (int x = 0; x < numeros.Length && found == false; x++)
                {
                    if (numeros[x].ToString() == cadena[i].ToString())
                    {
                        found = true;
                    }
                }
                if (found == false)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
