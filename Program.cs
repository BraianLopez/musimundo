using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musimundo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Producto producto1 = new Producto("play 3", 2);
            //Producto producto2 = new Producto("play 4", 1);
            //Producto producto3 = new Producto("play 5", 4);
            //Producto.AgregarProducto(producto1);
            //Producto.AgregarProducto(producto2);
            //Producto.AgregarProducto(producto3);
            try { 
            string rutaDelArchivo = "C:\\Users\\braia\\source\\repos\\Musimundo\\datos-inventario-musimundo.TXT";
            using(StreamReader sr = new StreamReader(rutaDelArchivo))
            {
                string linea;
                while((linea = sr.ReadLine()) != null)
                {
                    string[]  productoDelArchivo = linea.Split(';');
                    Producto producto = new Producto(productoDelArchivo[0], int.Parse(productoDelArchivo[1]));
                    Producto.AgregarProducto(producto);

                }
            }
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("Revisá la ruta del archivo:\n "+ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("No se encontró el archivo:\n " + ex.Message);
            }



            //Variables
            string mostrarMenu = "\nEscribí el numero de la opcion que deseas realizar:\n1- Ver Productos\n2- Agregar un producto \n3- Eliminar un producto\n4- Editar un producto\n5- Buscar\n6- Reportes y estadísticas \n0- Salir \n";
            int opcion;
            string descripcion;

            //Mensaje de bienvenida
            Console.WriteLine("Hola, bienvenido a  Musimundo!\n");

            //Bucle do-while para elegir la opcion a realizar(agregar producto, ver la lista de productos, etc)
            do
            {
                opcion = leerOpcion();

                switch (opcion)
                {
                    case 1:
                        Console.Clear();
                        Producto.MostrarProductos();
                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("Ingrese descripción del producto: \n");
                        descripcion = Console.ReadLine();
                        Console.Clear();
                        Console.WriteLine("\nIngrese stock inicial del producto: \n");
                        if (int.TryParse(Console.ReadLine(), out int stock))
                        {

                        }
                        Console.Clear();
                        Producto productoNuevo = new Producto(descripcion, stock);
                        Producto.AgregarProducto(productoNuevo);

                        break;

                    case 3:
                        Console.Clear();
                        Producto.MostrarProductos();

                        Console.WriteLine("Escribi el numero del producto que querés eliminar: \n");
                        if(int.TryParse(Console.ReadLine(), out int productoAEliminar)){

                        Producto.EliminarProducto(productoAEliminar);
                        }

                        break;
                    case 4:
                        Console.Clear();
                        Producto.MostrarProductos();
                        Console.WriteLine("0.Cancelar\n");
                        Console.WriteLine("ingresá el numero de el producto que deseas editar:\n");
                        int prodAEditar = int.Parse(Console.ReadLine());
                        if (prodAEditar == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("operacion cancelada");
                        }
                        else
                        {
                            Producto productoAEditar = Producto.TodosLosProductos[prodAEditar-1];
                            productoAEditar.EditarProducto();
                        }
                        break;
                    case 5: 
                        Console.Clear();
                        Producto.Buscar();
                        break;
                    case 6:
                        Console.Clear();
                        Producto.GenerarReporte();
                        break;

                }

            } while (opcion != 0);
            
            
            Producto.CargarArchivo();
            


            //Metodos


            int leerOpcion()
            {
                int numeroOpcion;
                while (true)
                {
                    Console.Write(mostrarMenu);
                    if (int.TryParse(Console.ReadLine(), out numeroOpcion))
                    {
                        if (numeroOpcion > 6)
                        {
                            Console.Clear();
                            Console.WriteLine("por favor, ingresa una opcion valida.");
                        }
                        return numeroOpcion;
                    }
                    Console.Clear();
                    Console.WriteLine("Por favor ingresa una opcion válida.");
                }
            }
        }   
    }
}
