using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Musimundo
{
    internal class Producto
    {
        public string Descripcion;

        private int Stock;

        //PROPIEDADES ESTATICAS
        public static int TotalProductos { get; private set; } = 0;
        public static List<Producto> TodosLosProductos = new List<Producto>();


        //GETTER Y SETTER PARA STOCK
        public int GetStock() { return Stock; }
        public void SetStock(int stock)
        {
            if (stock >= 0)

                Stock = stock;

            else

                throw new ArgumentException("el stock no puede ser negativo");
        }
        //CONSTRUCTOR
        public Producto(string descripcion, int stock)
        {
            Descripcion = descripcion;
            SetStock(stock);
            TotalProductos += stock;
        }

        public static void OrdenarPorStock()
        {
            int indiceProducto = 0;
            Console.WriteLine("Resumen del stock del inventario: \n");
            foreach (Producto producto in TodosLosProductos.OrderBy(producto => producto.GetStock()).ToList())
            {
                indiceProducto++;
                Console.WriteLine($"{indiceProducto} {producto.Descripcion} - stock actual:  {producto.GetStock()}");
            }
        }
        public static void AgregarProducto(Producto productoNuevo)
        {
            TodosLosProductos.Add(productoNuevo);
        }

        public static void MostrarProductos()

        {
            Console.WriteLine("PRODUCTOS(" + TodosLosProductos.Count() + ")\n");
            if (TotalProductos > 0)
            {
                int indiceProducto = 0;
                foreach (Producto producto in TodosLosProductos.OrderBy(producto => producto.Descripcion).ToList())
                {
                    indiceProducto++;
                    Console.WriteLine($"{indiceProducto} {producto.Descripcion} - stock actual:  {producto.GetStock()}");
                }
            }
            else
            {
                Console.WriteLine("Todavia no hay productos cargados");
            }
            Console.WriteLine("\n");

        }
        public static void ProductoConMayorStock()
        {
            if (TodosLosProductos.Count != 0)
            {
                Producto pConMasStock = TodosLosProductos[0];
                foreach (Producto producto in TodosLosProductos)
                {
                    if (producto.GetStock() > pConMasStock.GetStock())
                    {
                        pConMasStock = producto;
                    }
                }
                Console.WriteLine($"Producto con mayor stock: {pConMasStock.Descripcion} - stock: {pConMasStock.GetStock()}");


                // MEJOR OPCION
                // Se ordena de manera descendente, y se obtiene el primer elemento
                //Producto productoMayorStock = TodosLosProductos.OrderByDescending(p => p.Stock).FirstOrDefault();
            }
            else
            {
                Console.WriteLine($"Producto con mayor stock: No hay productos");
            }

        }
        public static void ProductoConMenorStock()
        {
            if (TodosLosProductos.Count != 0)
            {
                Producto pConMenosStock = TodosLosProductos[0];
                foreach (Producto producto in TodosLosProductos)
                {
                    if (producto.GetStock() < pConMenosStock.GetStock())
                    {
                        pConMenosStock = producto;
                    }
                }
                Console.WriteLine($"Producto con menor stock: {pConMenosStock.Descripcion} - stock: {pConMenosStock.GetStock()}");

                // MEJOR OPCION
                // Se ordena de manera ascendente, y se obtiene el primer elemento
                Producto productoMayorStock = TodosLosProductos.OrderBy(p => p.Stock).FirstOrDefault();
            }
            else
            {
                Console.WriteLine($"Producto con menor stock: No hay productos");
            }
        }

        public static void GenerarReporte()
        {
            Console.WriteLine($"Total de productos en inventario {TodosLosProductos.Sum(p => p.Stock)}\n");
            ProductoConMayorStock();
            ProductoConMenorStock();
            OrdenarPorStock();
        }

        public static void EliminarProducto(int productoAEliminar)
        {
            if (TodosLosProductos.Count() >= productoAEliminar)
            {
                Console.Clear();
                Console.WriteLine("¿Seguro que querés eliminar " + TodosLosProductos[productoAEliminar - 1].Descripcion + "?\n  1=si/2=no\n");
                int confirmaEliminar = int.Parse(Console.ReadLine());
                if (confirmaEliminar == 1)
                {
                    Console.Clear();
                    Console.WriteLine("Producto " + TodosLosProductos[productoAEliminar - 1].Descripcion + " eliminado con exito.\n");
                    TotalProductos -= TodosLosProductos[productoAEliminar - 1].GetStock(); //
                    TodosLosProductos.Remove(TodosLosProductos[productoAEliminar - 1]);
                }
                else if (confirmaEliminar == 2)
                {
                    Console.WriteLine("proceso 'eliminar' cancelado.\n");
                }

            }
            else
            {
                Console.WriteLine("El producto que intentas eliminar no existe.\n");
            }
        }


        public void EditarProducto()
        {
            Console.Clear();
            Console.WriteLine("la opcion elegida es: " + Descripcion);
            Console.WriteLine("ingresá el nuevo nombre del producto\n");
            Descripcion = Console.ReadLine().Trim(); 
            Console.WriteLine("Stock inicial: " + GetStock());
            Console.WriteLine("ingresá la nueva cantidad de stock del producto\n");
            SetStock(int.Parse(Console.ReadLine()));

            //if (Descripcion != "")
            //{
            //    Console.Clear();
            //    Console.WriteLine("Producto editado.\n");
            //    return;
            //}
            //else
            //{
            //    Console.Clear();
            //    Console.WriteLine("ingrese un  nombre valido para el producto. intente nuevamente");
            //}
        }
        public static void Buscar()
        {
            Console.Clear();
            Console.WriteLine("ingresá el producto que querés buscar");
            var productoBuscado = Console.ReadLine().Trim();
            var productoEncontrado = TodosLosProductos.Find(producto => producto.Descripcion.Contains(productoBuscado));
            if (productoEncontrado == null)
            {
                Console.WriteLine("No se encontró este producto. Intentá buscando otro o agregalo al sistema.");
            }
            else
            {
                Console.WriteLine($" {productoEncontrado.Descripcion} - stock actual:  {productoEncontrado.GetStock()}");
            }
        }

        public static void CargarArchivo()
        {
            TextWriter archivo = new StreamWriter("C:\\Users\\juanc\\OneDrive\\Documentos\\musimundo\\db.txt");
            TodosLosProductos.ForEach(producto => archivo.WriteLine(producto.Descripcion + ";" + producto.GetStock()));
            archivo.Close();
        }

    }
}

