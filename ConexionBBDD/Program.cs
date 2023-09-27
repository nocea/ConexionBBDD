using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConexionBBDD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int id_libro, edicion;
            string isbn;
            string titulo, autor, server, puerto, usuario, contraseña,bd,conexion;
            
            NpgsqlConnection conn=null;
            NpgsqlCommand query=null;
            NpgsqlDataReader dr=null;
            XmlDocument xml=new XmlDocument();
            XmlNodeList nodo;
            xml.Load("C:\\Users\\Puesto10\\Desktop\\GITHUB\\ConexionBBDD\\ConexionBBDD\\conexion.xml");
            Console.WriteLine("Se ha encontrado el archivo");
            nodo = xml.GetElementsByTagName("conexion");
            
            conexion = String.Format("Server={0};Port={1}; Username={2};Password={3};Database ={4}", server, puerto, usuario, contraseña, bd);
            try
            {
                conn = new NpgsqlConnection(conexion);
                Console.WriteLine("Abriendo conexión");
                conn.Open();
                query = new NpgsqlCommand("SELECT * FROM gbp_almacen.gbp_alm_cat_libros\r\nORDER BY id_libro ASC ", conn);
                dr = query.ExecuteReader();
                Console.WriteLine("Datos Leidos");
                while (dr.Read())
                {
                    id_libro = Convert.ToInt32(dr["id_libro"]);
                    isbn = Convert.ToString(dr["isbn"]);
                    edicion = Convert.ToInt32(dr["edicion"]);
                    titulo = Convert.ToString(dr["titulo"]);
                    autor = Convert.ToString(dr["autor"]);
                    Console.WriteLine("idLibro:{0};isbn:{1};edicion:{2};titulo:{3};autor:{4}", id_libro, isbn, edicion, titulo, autor);
                }
            }catch (Exception e) {
                Console.WriteLine("Se ha producido un error");
            }
            finally {
                Console.WriteLine("Data Reader Cerrado");
                dr.Close();
                Console.WriteLine("Conexion cerrada");
                conn.Close();
            }
            Console.ReadLine();
            
        }
    }
}
