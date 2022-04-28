using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioSocketsUtils
{
    public class ClienteCom
    {
        private Socket cliente;
        private StreamReader reader;
        private StreamWriter writer;

        public ClienteCom(Socket socket)
        {
            cliente = socket;
            Stream stream = new NetworkStream(cliente);
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
        }

        public bool Escribir(string mensaje)
        {
            try
            {
                writer.WriteLine(mensaje);
                writer.Flush();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string Leer()
        {
            try
            {
                return reader.ReadLine().Trim();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Desconectar()
        {
            try
            {
                cliente.Close();
            }
            catch (Exception)
            {

            }
        }
    }
}
