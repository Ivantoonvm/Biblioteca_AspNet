using Proyecto_biblioteca.Models;
using Newtonsoft.Json;
using System.Data;
using MySql.Data.MySqlClient;

namespace Proyecto_biblioteca.Logica;
    public class CategoriaLogica
    {
        private static CategoriaLogica instancia = null;

        public CategoriaLogica() {

        }

        public static CategoriaLogica Instancia {
            get {
                if (instancia == null) {
                    instancia = new CategoriaLogica();
                }

                return instancia;
            }
        }

        public bool Registrar(Categoria oCategoria)
        {
            Console.WriteLine();
            bool respuesta = true;
            using (MySqlConnection oConexion = new MySqlConnection(Conexion.CN))
            {
                try
                {   
                    Console.WriteLine(oCategoria.Descripcion + " Categoria");
                    MySqlCommand cmd = new MySqlCommand("sp_RegistrarCategoria", oConexion);
                    cmd.Parameters.AddWithValue("pDescripcion", oCategoria.Descripcion);
                    cmd.Parameters.Add("pResultado", MySqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    Console.WriteLine(cmd.Parameters["pResultado"].Value);
                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["pResultado"].Value);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool Modificar(Categoria oCategoria)
        {
            bool respuesta = true;
            using (MySqlConnection oConexion = new MySqlConnection(Conexion.CN))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("sp_ModificarCategoria", oConexion);
                    cmd.Parameters.AddWithValue("pIdCategoria", oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("pDescripcion", oCategoria.Descripcion);
                    cmd.Parameters.AddWithValue("pEstado", oCategoria.Estado);
                    cmd.Parameters.Add("pResultado", MySqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["pResultado"].Value);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    respuesta = false;
                }

            }

            return respuesta;

        }


        public List<Categoria> Listar()
{
    List<Categoria> Lista = new List<Categoria>();
    using (MySqlConnection oConexion = new MySqlConnection(Conexion.CN))
    {
        try
        {
            MySqlCommand cmd = new MySqlCommand("select IdCategoria, Descripcion, Estado from CATEGORIA", oConexion);
            cmd.CommandType = CommandType.Text;

            oConexion.Open();
            using (MySqlDataReader dr = cmd.ExecuteReader())
            {
                lock (Lista) // Agregar el bloqueo aquí
                {
                    while (dr.Read())
                    {
                        Lista.Add(new Categoria()
                        {
                            IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                            Descripcion = dr["Descripcion"].ToString(),
                            Estado = Convert.ToBoolean(dr["Estado"])
                        });
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Lista = new List<Categoria>();
        }
    }
    return Lista;
}

        public bool Eliminar(int id)
        {
            bool respuesta = true;
            using (MySqlConnection oConexion = new MySqlConnection(Conexion.CN))
            {
        
                try
                {
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM CATEGORIA WHERE IdCategoria = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                     oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    respuesta = false;
                }

            }

            return respuesta;

        }
    }
