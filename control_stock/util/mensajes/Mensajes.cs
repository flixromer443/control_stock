using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_stock.util
{
    internal class Mensajes
    {
        public static string SELECCIONAR_PRODUCTO = "Debe seleccionar un producto para continuar.";
        public static string PRODUCTO_ELIMINADO = "El producto seleccionado ha sido eliminado con exito.";
        public static string ACTUALIZAR_PRODUCTO = "Desea actualizar este producto?";
        public static string PRODUCTO_ACTUALIZADO = "El producto seleccionado ha sido actualizado con exito.";
        public static string AGREGAR_PRODUCTO = "Desea agregar este producto a la lista?";
        public static string PRODUCTO_AGREGADO = "Se ha agregado un nuevo producto.";
        public static string PRODUCTO_VENDIDO = "Se ha actulizado el stock de este producto.";
        public static string PRODUCTO_SIN_STOCK = "El producto seleccionado no cuenta con stock.";
        public static string CATEGORIA_VACIA = "Esta lista de productos se encuentra vacia.";
        public static string VENDER_CARRITO = "Desea confirmar esta venta?";
        public static string CARRITO_VENDIDO = "Operacion exitosa, se ha actualizado el stock";

        public static string ERROR_CADENAS = "Error, el uso de caracteres especiales no esta permitido.";

        public static string ERROR_DESCRIPCION = "Error, el campo descripción no es valido o se encuentra vacio.";
        public static string ERROR_PRECIO_COMPRA = "Error, el campo precio compra no es valido o se encuentra vacio.";
        public static string ERROR_PRECIO_VENTA = "Error, el campo precio venta no es valido o se encuentra vacio.";
        public static string ERROR_STOCK = "Error, el campo stock venta no es valido o se encuentra vacio.";
        public static string ERROR_UNIDADES = "La cantidad de unidades seleccionadas supera el stock de este producto. ";
        public static string ERROR_FECHA = "Error, el campo fecha se encuentra vacio o no es valido.";


    }
}
