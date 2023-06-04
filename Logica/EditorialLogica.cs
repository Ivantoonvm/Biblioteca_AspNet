using System.Data;
using MySql.Data.MySqlClient;
using Proyecto_biblioteca.Models;
namespace Proyecto_biblioteca.Logica;

public class EditorialLogica
    {

        private static EditorialLogica instancia = null;

        public EditorialLogica()
        {

        }

        public static EditorialLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new EditorialLogica();
                }

                return instancia;
            }
        }

        public bool Registrar(Editorial oEditorial)
        {
            bool respuesta = true;
            using (MySqlConnection oConexion = new MySqlConnection(Conexion.CN))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("sp_RegistrarEditorial", oConexion);
                    cmd.Parameters.AddWithValue("pDescripcion", oEditorial.Descripcion);
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

        public bool Modificar(Editorial oEditorial)
        {
            bool respuesta = true;
            using (MySqlConnection oConexion = new MySqlConnection(Conexion.CN))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("sp_ModificarEditorial", oConexion);
                    cmd.Parameters.AddWithValue("pIdEditorial", oEditorial.IdEditorial);
                    cmd.Parameters.AddWithValue("pDescripcion", oEditorial.Descripcion);
                    cmd.Parameters.AddWithValue("pEstado", oEditorial.Estado);
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


        public List<Editorial> Listar()
        {
            List<Editorial> Lista = new List<Editorial>();
            using (MySqlConnection oConexion = new MySqlConnection(Conexion.CN))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("select IdEditorial,Descripcion,Estado from EDITORIAL", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Editorial()
                            {
                                IdEditorial = Convert.ToInt32(dr["IdEditorial"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<Editorial>();
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
                    MySqlCommand cmd = new MySqlCommand("delete from EDITORIAL where IdEditorial = @id", oConexion);
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