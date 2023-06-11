using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Proyecto_biblioteca.Models;

namespace Proyecto_biblioteca.Logica;

    public class PersonaLogica
    {

        private static PersonaLogica instancia = null;

        public PersonaLogica()
        {

        }

        public static PersonaLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new PersonaLogica();
                }

                return instancia;
            }
        }


        public bool Registrar(Persona objeto)
        {
            bool respuesta = true;
            using (MySqlConnection oConexion = new MySqlConnection(Conexion.CN))
            {
                try
                {                
                    MySqlCommand cmd = new MySqlCommand("sp_RegistrarPersona", oConexion);
                    cmd.Parameters.AddWithValue("pNombre", objeto.Nombre);
                    cmd.Parameters.AddWithValue("pApellido", objeto.Apellido);
                    cmd.Parameters.AddWithValue("pCorreo", objeto.Correo);
                    cmd.Parameters.AddWithValue("pClave", objeto.Clave);
                    cmd.Parameters.AddWithValue("pCodigo", objeto.Codigo);
                    cmd.Parameters.AddWithValue("pIdTipoPersona", objeto.oTipoPersona.IdTipoPersona);
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

        public bool Modificar(Persona objeto)
        {
            bool respuesta = true;
            using (MySqlConnection oConexion = new MySqlConnection(Conexion.CN))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("sp_ModificarPersona", oConexion);
                    cmd.Parameters.AddWithValue("pIdPersona", objeto.IdPersona);
                    cmd.Parameters.AddWithValue("pNombre", objeto.Nombre);
                    cmd.Parameters.AddWithValue("pApellido", objeto.Apellido);
                    cmd.Parameters.AddWithValue("pCorreo", objeto.Correo);
                    cmd.Parameters.AddWithValue("pClave", objeto.Clave);
                    cmd.Parameters.AddWithValue("pIdTipoPersona", objeto.oTipoPersona.IdTipoPersona);
                    cmd.Parameters.AddWithValue("pEstado", objeto.Estado);
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


        public List<Persona> Listar()
        {
            List<Persona> Lista = new List<Persona>();
            using (MySqlConnection oConexion = new MySqlConnection(Conexion.CN))
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("select p.IdPersona,p.Nombre,p.Apellido,p.Correo,p.Clave,p.Codigo,tp.IdTipoPersona,tp.Descripcion, p.Estado from PERSONA p");
                    sb.AppendLine("inner join TIPO_PERSONA tp on tp.IdTipoPersona = p.IdTipoPersona");
                    MySqlCommand cmd = new MySqlCommand(sb.ToString(), oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Persona()
                            {
                                IdPersona = Convert.ToInt32(dr["IdPersona"]),
                                Nombre = dr["Nombre"].ToString(),
                                Apellido = dr["Apellido"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Codigo = dr["Codigo"].ToString(),
                                oTipoPersona = new TipoPersona() { IdTipoPersona = Convert.ToInt32(dr["IdTipoPersona"]), Descripcion = dr["Descripcion"].ToString() },
                                Estado = Convert.ToBoolean(dr["Estado"])
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Lista = new List<Persona>();
                }
            }

            return Lista;
        }

        public bool Eliminar(int IdPersona)
        {
            bool respuesta = true;
            using (MySqlConnection oConexion = new MySqlConnection(Conexion.CN))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("delete from PERSONA where IdPersona = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", IdPersona);
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
