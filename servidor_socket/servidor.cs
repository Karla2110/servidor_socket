using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Net;
using System.IO;
using System.Net.Sockets;

namespace servidor_socket
{

    public class servidor
    {

        private TcpListener mi_servidor; //Esta a la escucha y espera de una nueva conececion de clientes
        private TcpClient cliente = new TcpClient(); //Le da el permiso de coneccion al cliente
        private IPEndPoint locacion = new IPEndPoint(IPAddress.Any, 8088); //Direccion ip y puerto en el que se hospeda el servidor
        private List<conexion> list = new List<conexion>(); //Guarda los datos en una lista, se presentaran en el chat
        
        conexion con; //variable de conexion 

        public int puerto = 8088;

        private struct conexion
        {
            //Declaramos las variables para usarlas a lo largo de la estructura del servidor
            public NetworkStream flujo; 
            public StreamWriter datos_escritos; 
            public StreamReader datos_leidos;
            public string usuario;

        }

        public servidor()
        {
            inicio();
        }

        //metodo para iniciar la conexion del servidor
        public void inicio()
        { 
            Console.WriteLine("Se inicio el servidor en el puerto: " + puerto + ", esta listo para recibir conexiones");
            
            mi_servidor = new TcpListener(locacion); //le asigna la direccion de conexion del servidor
            mi_servidor.Start(); //y lo inicia

            while (true)
            {
                cliente = mi_servidor.AcceptTcpClient();
                con = new conexion();
                con.flujo = cliente.GetStream();
                con.datos_leidos = new StreamReader(con.flujo);
                con.datos_escritos = new StreamWriter(con.flujo);

                con.usuario = con.datos_leidos.ReadLine();

                list.Add(con);
                Console.WriteLine("El usuario " + con.usuario + " se a conectado.");

                Thread hilo = new Thread(escucha);
                hilo.Start();

            }
        }

        //Metodo que mantiene a la escucha el servidor para aceptar nuevas conexiones de los clientes
        public void escucha()
        {
            conexion con_host = con;
            do
            {
                try
                {
                    string msj = con_host.datos_leidos.ReadLine();
                    Console.WriteLine(con_host.usuario + ": " + msj);
                    foreach (conexion c in list)
                    {
                        try
                        {
                            c.datos_escritos.WriteLine(con_host.usuario + ": " + msj);
                            c.datos_escritos.Flush();
                        }
                        catch
                        {
                        }

                    }
                }
                catch
                {
                    list.Remove(con_host);
                    Console.WriteLine("El usuario "+ con.usuario + " se ha desconectado");
                    break;
                }


            }
            while (true);
            {

            }
        }
    }

   

}
