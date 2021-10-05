using Peliculas.WS.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Peliculas.WS
{
    /// <summary>
    /// Descripción breve de PeliculaService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class PeliculaService : System.Web.Services.WebService
    {

        [WebMethod]
        public string Bienvenidos()
        {
            return "Web Service para Peliculas :3";
        }

        [WebMethod(Description = "lista de peliculas")]
        public List<Pelicula> GetPeliculas()
        {
            PeliculasEntities conexion = new PeliculasEntities();
            var consulta = (from l in conexion.Peliculas select l).ToList();
            return consulta;
        }

        [WebMethod(Description = "Metodo para insertar una pelicula")]
        public bool CrearPelicula(string nombre, int añoEstreno, string pais, string categoria, string idioma)
        {
            try
            {
                using (PeliculasEntities conexion = new PeliculasEntities())
                {
                    Pelicula nuevaPelicula = new Pelicula();
                    nuevaPelicula.ID = Guid.NewGuid();
                    nuevaPelicula.Nombre = nombre;
                    nuevaPelicula.AnioEstreno = añoEstreno;
                    nuevaPelicula.Pais = pais;
                    nuevaPelicula.Categoria = categoria;
                    nuevaPelicula.Idioma = idioma;
                    conexion.Peliculas.Add(nuevaPelicula);
                    conexion.SaveChanges();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;

            }
        }


        [WebMethod(Description = "Modificar datos de pelicula")]
        public bool UpdatePelicula(Guid id, string nombre, int añoEstreno, string pais, string categoria, string idioma)
        {
            try
            {
                using (PeliculasEntities conexion = new PeliculasEntities())
                {
                    var consulta = (from l in conexion.Peliculas where l.ID == id select l).FirstOrDefault();
                    if (consulta != null)
                    {

                        consulta.Nombre = nombre;
                        consulta.AnioEstreno = añoEstreno;
                        consulta.Pais = pais;
                        consulta.Categoria = categoria;
                        consulta.Idioma = idioma;
                        conexion.SaveChanges();
                        return true;

                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        [WebMethod(Description = "Eliminar pelicula")]
        public bool DeletePelicula(Guid id)
        {
            try
            {
                using (PeliculasEntities conexion = new PeliculasEntities())
                {
                    var consulta = (from l in conexion.Peliculas where l.ID == id select l).FirstOrDefault();
                    if (consulta != null)
                    {
                        conexion.Peliculas.Remove(consulta);

                        conexion.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }    
}
