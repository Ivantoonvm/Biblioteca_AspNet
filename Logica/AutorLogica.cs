using Proyecto_biblioteca.Models;
using System.Data;
using MySql.Data.MySqlClient;

namespace Proyecto_biblioteca.Logica;
    public class AutorLogica
    {

        private static AutorLogica instancia = null;

        public AutorLogica()
        {

        }

        public static AutorLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new AutorLogica();
                }

                return instancia;
            }
        }

        public bool Registrar(Autor oAutor)
        {
            bool respuesta = true;
            using (MySqlConnection oConexion = new MySqlConnection(Conexion.CN))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("sp_RegistrarAutor", oConexion);
                    cmd.Parameters.AddWithValue("pDescripcion", oAutor.Descripcion);
                    cmd.Parameters.Add("pResultado", MySqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["pResultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool Modificar(Autor oAutor)
        {
            bool respuesta = true;
            using (MySqlConnection oConexion = new MySqlConnection(Conexion.CN))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("sp_ModificarAutor", oConexion);
                    cmd.Parameters.AddWithValue("pIdAutor", oAutor.IdAutor);
                    cmd.Parameters.AddWithValue("pDescripcion", oAutor.Descripcion);
                    cmd.Parameters.AddWithValue("pEstado", oAutor.Estado);
                    cmd.Parameters.Add("pResultado", MySqlDbType.Bit).Direction = ParameterDirection.Output;

                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["pResultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }


        public List<Autor> Listar()
        {
            List<Autor> Lista = new List<Autor>();
            using (MySqlConnection oConexion = new MySqlConnection(Conexion.CN))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("select IdAutor,Descripcion,Estado from AUTOR", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Autor()
                            {
                                IdAutor = Convert.ToInt32(dr["IdAutor"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Autor>();
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
                    MySqlCommand cmd = new MySqlCommand("delete from AUTOR where IdAutor = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = true;

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }
    }
