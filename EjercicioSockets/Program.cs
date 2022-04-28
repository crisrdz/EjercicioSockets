using EjercicioSocketsUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioSocket
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            Console.WriteLine("Iniciando Servidor en puerto {0}", puerto);
            ServerSocket servidor = new ServerSocket(puerto);

            if (servidor.Iniciar())
            {
                Console.WriteLine("Servidor iniciado");
                while (true)
                {
                    Console.WriteLine("Esperando cliente");
                    Socket socketCliente = servidor.ObtenerCliente();

                    ClienteCom cliente = new ClienteCom(socketCliente);
                    
                    Console.WriteLine("Cliente conectado, esperando mensaje de cliente...");

                    //Para Hercules:   <CR><LF>

                    string mensajeCliente, mensajeServidor;
                    while (true)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        mensajeCliente = cliente.Leer();
                        if(mensajeCliente.Trim().ToLower() == "chao")
                        {
                            Console.WriteLine("Cliente dijo 'chao', desconectando...");
                            break;
                        }
                        Console.WriteLine("Cliente: {0}", mensajeCliente);
                        Console.Write("Servidor: ");
                        mensajeServidor = Console.ReadLine();
                        
                        cliente.Escribir(mensajeServidor);
                        if (mensajeServidor.Trim().ToLower() == "chao")
                        {
                            break;
                        }

                    }

                    Console.ForegroundColor = ConsoleColor.Gray;
                    cliente.Desconectar();
                }
            }
            else
            {
                Console.WriteLine("Error, el puerto {0} está en uso...", puerto);
            }
        }
    }
}
