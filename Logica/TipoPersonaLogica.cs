using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Proyecto_biblioteca.Models;

namespace Proyecto_biblioteca.Logica;

    public class TipoPersonaLogica
    {
        private static TipoPersonaLogica instancia = null;

        public TipoPersonaLogica()
        {

        }

        public static TipoPersonaLogica Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new TipoPersonaLogica();
                }

                return instancia;
            }
        }

        public List<TipoPersona> Listar()
        {
            List<TipoPersona> Lista = new List<TipoPersona>();
            using (MySqlConnection oConexion = new MySqlConnection(Conexion.CN))
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand("select IdTipoPersona,Descripcion from TIPO_PERSONA", oConexion);
                    cmd.CommandType = CommandType.Text;

                    oConexion.Open();
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new TipoPersona()
                            {
                                IdTipoPersona = Convert.ToInt32(dr["IdTipoPersona"]),
                                Descripcion = dr["Descripcion"].ToString()
                            });
                        }
                    }

                }
                catch (Exception ex)
                {
                    Lista = new List<TipoPersona>();
                }
            }
            return Lista;
        }
    }
