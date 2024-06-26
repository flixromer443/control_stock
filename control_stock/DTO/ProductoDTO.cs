﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_stock.DTO
{
    
    public class ProductoDTO
    {
        private int id;
        private int categoriaId;
        private string descripcion;
        private string precioCompra;
        private string precioVenta;
        private int stock;

        public ProductoDTO()
        {
        }

        public int Id { get => id; set => id = value; }
        public int CategoriaId { get => categoriaId; set => categoriaId = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string PrecioCompra { get => precioCompra; set => precioCompra = value; }
        public string PrecioVenta { get => precioVenta; set => precioVenta = value; }
        public int Stock { get => stock; set => stock = value; }

        public static implicit operator ProductoDTO(bool v)
        {
            throw new NotImplementedException();
        }
    }
}
