using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioSocketsUtils
{
    public class ServerSocket
    {
        private int puerto;
        private Socket servidor;

        public ServerSocket (int puerto)
        {
            this.puerto = puerto;
        }

        // Iniciar la conexión del servidor, tomando posesión del puerto
        // devolverá true o false según tome la conexión
        public bool Iniciar()
        {
            try
            {
                this.servidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                
                // Tomar posesión del puerto
                this.servidor.Bind(new IPEndPoint(IPAddress.Any, this.puerto));

                // Definir cola de espera
                this.servidor.Listen(10);

                return true;
            }catch (SocketException)
            {
                return false;
            }
        }

        public Socket ObtenerCliente()
        {
            return this.servidor.Accept();
        }

        public void Cerrar()
        {
            try
            {
                this.servidor.Close();
            }catch(Exception)
            {

            }
        }
    }
}
