using System;
using System.Data;
using System.Text;
using Proyecto_biblioteca.Models;
using System.Globalization;
using MySql.Data.MySqlClient;

namespace Proyecto_biblioteca.Logica
{
    public class PrestamoLogica
    {
        private static PrestamoLogica instancia = null;

        public PrestamoLogica()
        {

        }

        public static PrestamoLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new PrestamoLogica();
                }

                return instancia;
            }
        }

        public bool Registrar(Prestamo objeto)
        {
            bool respuesta = true;
            using (MySqlConnection oConexion = new MySqlConnection(Conexion.CN))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("sp_RegistrarPrestamo", oConexion);
                    cmd.Parameters.AddWithValue("pIdEstadoPrestamo", objeto.oEstadoPrestamo.IdEstadoPrestamo);
                    cmd.Parameters.AddWithValue("pIdPersona", objeto.oPersona.IdPersona);
                    cmd.Parameters.AddWithValue("pIdLibro", objeto.oLibro.IdLibro);
                    cmd.Parameters.AddWithValue("pFechaDevolucion", Convert.ToDateTime(objeto.TextoFechaDevolucion,new CultureInfo("es-PE")) );
                    cmd.Parameters.AddWithValue("pEstadoEntregado", objeto.EstadoEntregado);
                    cmd.Parameters.Add("pResultado", MySqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["pResultado"].Value);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERR" + ex);
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool Existe(Prestamo objeto)
        {
            bool respuesta = true;
            using (MySqlConnection oConexion = new MySqlConnection(Conexion.CN))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("sp_existePrestamo", oConexion);
                    cmd.Parameters.AddWithValue("pIdPersona", objeto.oPersona.IdPersona);
                    cmd.Parameters.AddWithValue("pIdLibro", objeto.oLibro.IdLibro);
                    cmd.Parameters.Add("pResultado", MySqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["pResultado"].Value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERR" + ex);
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public List<EstadoPrestamo> ListarEstados()
        {
            List<EstadoPrestamo> Lista = new List<EstadoPrestamo>();
            using (MySqlConnection oConexion = new MySqlConnection(Conexion.CN))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("select IdEstadoPrestamo,Descripcion from ESTADO_PRESTAMO", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new EstadoPrestamo()
                            {
                                IdEstadoPrestamo = Convert.ToInt32(dr["IdEstadoPrestamo"]),
                                Descripcion = dr["Descripcion"].ToString()
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERR" + ex);
                    Lista = new List<EstadoPrestamo>();
                }
            }
            return Lista;
        }


        public List<Prestamo> Listar(int idestadoprestamo, int idpersona)
        {
            List<Prestamo> Lista = new List<Prestamo>();
            using (MySqlConnection oConexion = new MySqlConnection(Conexion.CN))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT p.IdPrestamo, ep.IdEstadoPrestamo, ep.Descripcion, pe.Codigo, pe.Nombre, pe.Apellido, li.Titulo, DATE_FORMAT(p.FechaDevolucion, '%d/%m/%Y') AS FechaDevolucion, DATE_FORMAT(p.FechaConfirmacionDevolucion, '%d/%m/%Y') AS FechaConfirmacionDevolucion, p.EstadoEntregado, p.EstadoRecibido");
                    query.AppendLine("FROM PRESTAMO p");
                    query.AppendLine("INNER JOIN ESTADO_PRESTAMO ep ON ep.IdEstadoPrestamo = p.IdEstadoPrestamo");
                    query.AppendLine("INNER JOIN PERSONA pe ON pe.IdPersona = p.IdPersona");
                    query.AppendLine("INNER JOIN LIBRO li ON li.IdLibro = p.IdLibro");
                    query.AppendLine("WHERE ep.IdEstadoPrestamo = IF(@idestadoprestamo = 0, ep.IdEstadoPrestamo, @idestadoprestamo)");
                    query.AppendLine("AND pe.IdPersona = IF(@idpersona = 0, pe.IdPersona, @idpersona)");
                    MySqlCommand cmd = new MySqlCommand(query.ToString(), oConexion);
                    cmd.Parameters.AddWithValue("@idestadoprestamo", idestadoprestamo);
                    cmd.Parameters.AddWithValue("@idpersona", idpersona);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new Prestamo()
                            {
                                IdPrestamo = Convert.ToInt32(dr["IdPrestamo"]),
                                oEstadoPrestamo = new EstadoPrestamo() { IdEstadoPrestamo= Convert.ToInt32(dr["IdEstadoPrestamo"]),  Descripcion = dr["Descripcion"].ToString() },
                                oPersona = new Persona() { Codigo = dr["Codigo"].ToString(), Nombre = dr["Nombre"].ToString(), Apellido = dr["Apellido"].ToString() },
                                oLibro= new Libro() { Titulo = dr["Titulo"].ToString() },
                                TextoFechaDevolucion = dr["FechaDevolucion"].ToString(),
                                TextoFechaConfirmacionDevolucion = dr["FechaConfirmacionDevolucion"].ToString(),
                                EstadoEntregado = dr["EstadoEntregado"].ToString(),
                                EstadoRecibido = dr["EstadoRecibido"].ToString()
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERR" + ex);
                    Lista = new List<Prestamo>();
                }
            }
            return Lista;
        }

        public bool Devolver(string estadorecibido, int idprestamo)
        {
            bool respuesta = true;
            using (MySqlConnection oConexion = new MySqlConnection(Conexion.CN))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("update PRESTAMO set IdEstadoPrestamo = 2 ,FechaConfirmacionDevolucion = CURRENT_TIMESTAMP,EstadoRecibido =@estadorecibido");
                    query.AppendLine("where IdPrestamo = @idprestamo");

                    MySqlCommand cmd = new MySqlCommand(query.ToString(), oConexion);
                    cmd.Parameters.AddWithValue("@estadorecibido", estadorecibido);
                    cmd.Parameters.AddWithValue("@idprestamo", idprestamo);
                    cmd.CommandType = CommandType.Text;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERR" + ex);
                    respuesta = false;
                }
            }
            return respuesta;
        }



    }
}