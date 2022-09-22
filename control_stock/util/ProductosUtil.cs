﻿using control_stock.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_stock.util
{
    internal class ProductosUtil
    {
        private GenerardorDeMensajes generadorDeMensajes = new GenerardorDeMensajes();
        public Boolean validarProductoSeleccionado(ProductoDTO nuevoProducto)
        {

            if (nuevoProducto.Descripcion == null || nuevoProducto.Descripcion.Length == 0)
            {
                return true;
            }
            if (nuevoProducto.PrecioCompra == null || nuevoProducto.PrecioCompra.Length == 0)
            {
                return true;
            }
            if (nuevoProducto.PrecioVenta == null || nuevoProducto.PrecioVenta.Length == 0)
            {
                return true;
            }
            if (nuevoProducto.Stock < 0)
            {
                return true;
            }
            return false;
        }
        public string reducirCaracteresDescripcion(string descripcion)
        {
            return descripcion.Substring(0, 20) + ".";
        }
        public string generarPrecio(string precio)
        {
            StringBuilder precioVenta = new StringBuilder();
            precioVenta.Append("$" + precio);
            return precioVenta.ToString();
        }
    }
}