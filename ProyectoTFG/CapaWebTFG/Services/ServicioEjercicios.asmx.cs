using Newtonsoft.Json;
using ProyectoTFG.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using ProyectoTFG.DAL;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace CapaWebTFG.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    public class ServicioEjercicios : WebService
    {

        private bool VerificarCookie()
        {
            var cookie = HttpContext.Current.Request.Cookies["UserId"];
            return cookie != null && !string.IsNullOrEmpty(cookie.Value);
        }

        private void RedirigirSiNoAutenticado()
        {
            if (!VerificarCookie())
            {
                HttpContext.Current.Response.StatusCode = 401; // No autorizado
                HttpContext.Current.Response.Redirect("../views/IniciarSesion.aspx"); // Redirige a la página de inicio de sesión
                HttpContext.Current.Response.End(); // Termina la respuesta
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string ObtenerEjercicios()
        {
            RedirigirSiNoAutenticado();
            try
            {
                using (var context = new Contexto())
                {
                    var ejercicios = context.Ejercicios
                        .Select(e => new
                        {
                            e.Ejercicios_Id,
                            e.Nombre,
                            e.Descripcion,
                            e.Imagen_Ejercicio
                        }).ToList();

                    return JsonConvert.SerializeObject(new { success = true, data = ejercicios });

                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { success = false, message = ex.Message });
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string ObtenerEjerciciosUser(string categoria = "all")
        {
            RedirigirSiNoAutenticado();

            try
            {
                using (var context = new Contexto())
                {
                    // Obtener los ejercicios de la base de datos
                    var ejerciciosQuery = context.Ejercicios
                        .Select(e => new
                        {
                            e.Ejercicios_Id,
                            e.Nombre,
                            e.Descripcion,
                            e.Imagen_Ejercicio
                        });

                    // Filtrar los ejercicios por categoría si no es "all"
                    if (categoria != "all")
                    {
                        ejerciciosQuery = ejerciciosQuery.Where(e => e.Descripcion.ToLower().Contains(categoria.ToLower()));
                    }

                    var ejercicios = ejerciciosQuery.ToList();

                    return JsonConvert.SerializeObject(new { success = true, data = ejercicios });
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores, devolviendo un mensaje de error
                return JsonConvert.SerializeObject(new { success = false, message = ex.Message });
            }
        }



        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string BorrarEjercicio(int id)
        {
            try
            {
                using (var context = new Contexto())
                {
                    var ejercicios = context.Ejercicios.Find(id);
                    if (ejercicios != null)
                    {
                        context.Ejercicios.Remove(ejercicios);
                        context.SaveChanges();
                        return JsonConvert.SerializeObject(new { success = true });
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new { success = false, message = "Ejercicio no encontrado." });
                    }
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { success = false, message = ex.Message });
            }
        }
        [WebMethod]
        public string AgregarEjercicio()
        {
            try
            {
                HttpContext context = HttpContext.Current;

                // Obtener los campos de texto
                var Nombre = context.Request.Form["Nombre"];
                var Descripcion = context.Request.Form["Descripcion"];

                // Obtener el archivo de imagen
                var imagen = context.Request.Files["Imagen_Ejercicio"];
                if (imagen != null && imagen.ContentLength > 0)
                {
                    // Crear el nombre del archivo y guardarlo en el servidor
                    string fileName = Path.GetFileName(imagen.FileName);
                    string filePath = Path.Combine(context.Server.MapPath("~/views/images/"), fileName); 
                    imagen.SaveAs(filePath);

                    // Guardar solo el path en la base de datos
                    var nuevaEjercicio = new Ejercicios
                    {
                        Nombre = Nombre,
                        Descripcion = Descripcion,
                        Imagen_Ejercicio = "/views/images/" + fileName
                    };

                    var validationResults = new List<ValidationResult>();
                    var validationContext = new ValidationContext(nuevaEjercicio);
                    if (!Validator.TryValidateObject(nuevaEjercicio, validationContext, validationResults, true))
                    {
                        return JsonConvert.SerializeObject(new { success = false, message = "Validación fallida: " + string.Join(", ", validationResults.Select(v => v.ErrorMessage)) });
                    }

                    using (var dbContext = new Contexto())
                    {
                        dbContext.Ejercicios.Add(nuevaEjercicio);
                        dbContext.SaveChanges();
                    }

                    return JsonConvert.SerializeObject(new { success = true });
                }

                return JsonConvert.SerializeObject(new { success = false, message = "No se ha recibido una imagen válida." });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { success = false, message = "Error: " + ex.Message });
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string EditarEjercicio(int Ejercicios_Id, string Nombre, string Descripcion)
        {
            try
            {
                HttpContext context = HttpContext.Current;

                using (var dbContext = new Contexto())
                {
                    var ejercicio = dbContext.Ejercicios.Find(Ejercicios_Id);
                    if (ejercicio != null)
                    {
                        // Actualizar los campos de texto
                        ejercicio.Nombre = Nombre;
                        ejercicio.Descripcion = Descripcion;

                        // Verificar si se ha proporcionado una nueva imagen
                        var nuevaImagen = context.Request.Files["Imagen_Ejercicio"];
                        if (nuevaImagen != null && nuevaImagen.ContentLength > 0)
                        {
                            // Eliminar la imagen anterior si existe
                            string oldFilePath = context.Server.MapPath("~" + ejercicio.Imagen_Ejercicio);
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }

                            // Guardar la nueva imagen
                            string fileName = Path.GetFileName(nuevaImagen.FileName);
                            string newFilePath = Path.Combine(context.Server.MapPath("~/views/images/"), fileName);
                            nuevaImagen.SaveAs(newFilePath);

                            // Actualizar la ruta de la imagen en la base de datos
                            ejercicio.Imagen_Ejercicio = "/views/images/" + fileName;
                        }

                        // Guardar los cambios
                        dbContext.SaveChanges();

                        return JsonConvert.SerializeObject(new { success = true });
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new { success = false, message = "Ejercicio no encontrado." });
                    }
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { success = false, message = ex.Message });
            }
        }



        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string ObtenerEjercicioPorId(int id)
        {
                        RedirigirSiNoAutenticado();

            try
            {
                using (var context = new Contexto())
                {
                    var ejercicio = context.Ejercicios.Find(id);

                    if (ejercicio != null)
                    {
                        return JsonConvert.SerializeObject(new { success = true, data = ejercicio });
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new { success = false, message = "Ejercicio no encontrada." });
                    }
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { success = false, message = ex.Message });
            }
        }

    }
}
