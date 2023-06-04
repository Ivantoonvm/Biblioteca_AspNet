using MySql.Data.MySqlClient;
using Proyecto_biblioteca.Models;
using System.Text;
using System.Data;



namespace Proyecto_biblioteca.Logica;
    public class LibroLogica
    {

        private static LibroLogica instancia = null;

        public LibroLogica()
        {

        }

        public static LibroLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new LibroLogica();
                }

                return instancia;
            }
        }

        public List<Libro> Listar()
        {

            List<Libro> rptListaLibro = new List<Libro>();
            using (MySqlConnection oConexion = new MySqlConnection(Conexion.CN))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SELECT l.IdLibro, l.Titulo, l.RutaPortada, l.NombrePortada,");
                sb.AppendLine("a.IdAutor, a.Descripcion AS DescripcionAutor,");
                sb.AppendLine("c.IdCategoria, c.Descripcion AS DescripcionCategoria,");
                sb.AppendLine("e.IdEditorial, e.Descripcion AS DescripcionEditorial,");
                sb.AppendLine("l.Ubicacion, l.Ejemplares, l.Estado");
                sb.AppendLine("FROM LIBRO l");
                sb.AppendLine("INNER JOIN AUTOR a ON a.IdAutor = l.IdAutor");
                sb.AppendLine("INNER JOIN CATEGORIA c ON c.IdCategoria = l.IdCategoria");
                sb.AppendLine("INNER JOIN EDITORIAL e ON e.IdEditorial = l.IdEditorial");
                string query = sb.ToString();
                MySqlCommand cmd = new MySqlCommand(query, oConexion);
                cmd.CommandType = CommandType.Text;

                try
                {
                    oConexion.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaLibro.Add(new Libro()
                        {
                            IdLibro = Convert.ToInt32(dr["IdLibro"].ToString()),
                            Titulo = dr["Titulo"].ToString(),
                            RutaPortada = dr["RutaPortada"].ToString(),
                            NombrePortada = dr["NombrePortada"].ToString(),
                            oAutor = new Autor() { IdAutor = Convert.ToInt32(dr["IdAutor"].ToString()), Descripcion = dr["DescripcionAutor"].ToString() },
                            oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"].ToString()), Descripcion = dr["DescripcionCategoria"].ToString() },
                            oEditorial = new Editorial() { IdEditorial = Convert.ToInt32(dr["IdEditorial"].ToString()), Descripcion = dr["DescripcionEditorial"].ToString() },
                            Ubicacion = dr["Ubicacion"].ToString(),
                            Ejemplares = Convert.ToInt32(dr["Ejemplares"].ToString()),
                            base64 = Utilidades.convertirBase64(Path.Combine(dr["RutaPortada"].ToString(), dr["NombrePortada"].ToString())),
                            extension = Path.GetExtension(dr["NombrePortada"].ToString()).Replace(".",""),
                            Estado = dr.GetBoolean(dr.GetOrdinal("Estado"))
                        });
                    }
                    dr.Close();

                    return rptListaLibro;

                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                    rptListaLibro = null;
                    return rptListaLibro;
                }
            }
        }


        public int Registrar(Libro objeto)
        {
            int respuesta = 0;
            using (MySqlConnection oConexion = new MySqlConnection(Conexion.CN))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("sp_registrarLibro", oConexion);
                    cmd.Parameters.AddWithValue("pTitulo", objeto.Titulo);
                    cmd.Parameters.AddWithValue("pRutaPortada", objeto.RutaPortada);
                    cmd.Parameters.AddWithValue("pNombrePortada", objeto.NombrePortada);
                    cmd.Parameters.AddWithValue("pIdAutor", objeto.oAutor.IdAutor);
                    cmd.Parameters.AddWithValue("pIdCategoria", objeto.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("pIdEditorial", objeto.oEditorial.IdEditorial);
                    cmd.Parameters.AddWithValue("pUbicacion", objeto.Ubicacion);
                    cmd.Parameters.AddWithValue("pEjemplares", objeto.Ejemplares);
                    cmd.Parameters.Add("pResultado", MySqlDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToInt32(cmd.Parameters["pResultado"].Value);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERRR => "+ ex);
                    respuesta = 0;
                }
            }
            return respuesta;
        }


        public bool Modificar(Libro objeto)
        {
            bool respuesta = false;
            using (MySqlConnection oConexion = new MySqlConnection(Conexion.CN))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("sp_modificarLibro", oConexion);
                    cmd.Parameters.AddWithValue("p_IdLibro", objeto.IdLibro);
                    cmd.Parameters.AddWithValue("p_Titulo", objeto.Titulo);
                    cmd.Parameters.AddWithValue("p_IdAutor", objeto.oAutor.IdAutor);
                    cmd.Parameters.AddWithValue("p_IdCategoria", objeto.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("p_IdEditorial", objeto.oEditorial.IdEditorial);
                    cmd.Parameters.AddWithValue("p_Ubicacion", objeto.Ubicacion);
                    cmd.Parameters.AddWithValue("p_Ejemplares", objeto.Ejemplares);
                    cmd.Parameters.AddWithValue("p_Estado", objeto.Estado);
                    cmd.Parameters.Add("p_Resultado", MySqlDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["p_Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool ActualizarRutaImagen(Libro objeto)
        {
            bool respuesta = true;
            using (MySqlConnection oConexion = new MySqlConnection(Conexion.CN))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("sp_actualizarRutaImagen", oConexion);
                    cmd.Parameters.AddWithValue("p_IdLibro", objeto.IdLibro);
                    cmd.Parameters.AddWithValue("p_NombrePortada", objeto.NombrePortada);
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERx" + ex);
                    respuesta = false;
                }
            }
            return respuesta;
        }


        public bool Eliminar(int IdLibro)
        {
            bool respuesta = true;
            using (MySqlConnection oConexion = new MySqlConnection(Conexion.CN))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("DELETE FROM LIBRO where IdLibro = @id", oConexion);
                    cmd.Parameters.AddWithValue("@id", IdLibro);
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
