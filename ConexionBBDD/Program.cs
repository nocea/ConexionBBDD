using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ConexionBBDD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int id_libro, edicion;
            string isbn;
            string titulo, autor;
            NpgsqlConnection conn=null;
            NpgsqlCommand query=null;
            NpgsqlDataReader dr=null;
            
            try
            {
                conn = new NpgsqlConnection("Server=localhost;Port=5432; Username=postgres;Password=mariomanu7.;Database = gestorBibliotecaPersonal");
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
