using ClienteEjercicioSocket.Comunicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteEjercicioSocket
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            
            bool seguir = false;
            ClienteSocket clienteSocket=null;
            string servidor ="";
            int puerto=0;

            while(!seguir)
            {
                try
                {
                    Console.WriteLine("Por favor, escriba la IP del servidor al cual conectarse: ");
                    servidor = Console.ReadLine();
                    Console.WriteLine("Por favor, escriba el puerto al cual conectarse: ");
                    puerto = Convert.ToInt32(Console.ReadLine());
                    clienteSocket = new ClienteSocket(servidor, puerto);
                    if(clienteSocket.Conectar())
                    {
                        seguir = true;
                    }
                    else
                    {
                        Console.WriteLine("Error de comunicación");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Por favor ingrese datos válidos");
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Conectado a Servidor {0} en puerto {1}", servidor, puerto);
            
            Console.WriteLine("Escriba su mensaje a continuación");
            Comunicacion(clienteSocket);
            
            Console.ReadKey();
        }

        public static void Comunicacion(ClienteSocket cliente)
        {
            while (true)
            {
                Console.Write("Cliente: ");
                string mensajeCliente = Console.ReadLine().Trim();
                cliente.Escribir(mensajeCliente);

                if (mensajeCliente.ToLower() == "chao")
                {
                    cliente.Desconectar();
                    Console.WriteLine("Desconectado");
                    break;
                }

                string mensajeServidor = cliente.Leer();
                Console.WriteLine("Servidor: {0}", mensajeServidor);
                if (mensajeServidor.ToLower() == "chao")
                {
                    cliente.Desconectar();
                    Console.WriteLine("Desconectado");
                    break;
                }
                
            }
            
        }
    }

}
