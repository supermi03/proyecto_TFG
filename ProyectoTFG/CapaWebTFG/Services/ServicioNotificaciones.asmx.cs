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
using System.Globalization;
using System.Web.Services.Description;
using System.Data.Entity.Infrastructure;

namespace CapaWebTFG.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    public class ServicioNotificaciones : WebService
    {
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string ObtenerNotificaciones()
        {
            try
            {
                using (var context = new Contexto())
                {
                    var notificaciones = context.Notificaciones
                        .Select(n => new
                        {
                            n.Notificaciones_Id,
                            n.Mensaje,
                            n.Fecha,
                            n.Persona_Id,
                        }).ToList();

                    return JsonConvert.SerializeObject(new { success = true, data = notificaciones });

                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { success = false, message = ex.Message });
            }
        }

   [WebMethod]
[ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
public string BorrarNotificacion(int id)
{
    try
    {
        using (var context = new Contexto())
        {
            var notificacion = context.Notificaciones.Find(id);
            if (notificacion != null)
            {
                context.Notificaciones.Remove(notificacion);
                context.SaveChanges();
                return JsonConvert.SerializeObject(new { success = true });
            }
            else
            {
                return JsonConvert.SerializeObject(new { success = false, message = "Notificación no encontrada." });
            }
        }
    }
    catch (Exception ex)
    {
        // Aquí puedes retornar más detalles del error si es necesario para depuración, como ex.StackTrace
        return JsonConvert.SerializeObject(new { success = false, message = $"Error al borrar la notificación: {ex.Message}" });
    }
}



        // Método para hashear la contraseña usando BCrypt
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }


        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        [WebMethod]
        public string AgregarNotificaciones(string Mensaje, int Persona_Id, DateTime Fecha)
        {
            try
            {
                // Crear un nuevo objeto notificación
                var nuevaNotificacion = new Notificaciones
                {
                    Fecha = Fecha,
                    Mensaje = Mensaje,
                    Persona_Id = Persona_Id
                };

                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(nuevaNotificacion);
                if (!Validator.TryValidateObject(nuevaNotificacion, validationContext, validationResults, true))
                {
                    return JsonConvert.SerializeObject(new { success = false, message = "Validación fallida: " + string.Join(", ", validationResults.Select(v => v.ErrorMessage)) });
                }

                // Añadir la notificación a la base de datos
                using (var context = new Contexto())
                {
                    context.Notificaciones.Add(nuevaNotificacion);
                    context.SaveChanges();
                }

                return JsonConvert.SerializeObject(new { success = true });
            }
            catch (FormatException)
            {
                return JsonConvert.SerializeObject(new { success = false, message = "Formato de fecha no válido. Use el formato dd/MM/yyyy." });
            }
            catch (ArgumentNullException ex)
            {
                return JsonConvert.SerializeObject(new { success = false, message = "Un parámetro requerido es nulo: " + ex.ParamName });
            }
            catch (Exception ex)
            {
                // Loguear el error para auditoría
                return JsonConvert.SerializeObject(new { success = false, message = "Ha ocurrido un error inesperado: " + ex.Message });
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string EditarNotificaciones(int notificaciones_Id, string mensaje, DateTime fecha, int persona_Id)
        {
            try
            {
                using (var context = new Contexto())
                {
                    // Buscar la notificación por su ID
                    var notificacion = context.Notificaciones.Find(notificaciones_Id);
                    if (notificacion != null)
                    {
                        // Actualizar los campos de la notificación
                        notificacion.Mensaje = mensaje;
                        notificacion.Fecha = fecha;
                        notificacion.Persona_Id = persona_Id;

                        // Guardar los cambios en la base de datos
                        context.SaveChanges();

                        return JsonConvert.SerializeObject(new { success = true });
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new { success = false, message = "Notificación no encontrada." });
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                // Manejo específico para excepciones de actualización
                return JsonConvert.SerializeObject(new { success = false, message = "Error al actualizar la base de datos: " + dbEx.Message });
            }
            catch (Exception ex)
            {
                // Manejo genérico de excepciones
                return JsonConvert.SerializeObject(new { success = false, message = "Error: " + ex.Message });
            }
        }



        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string ObtenerNotificacionPorId(int id)
        {
            try
            {
                using (var context = new Contexto())
                {
                    var notificacion = context.Notificaciones.Find(id);
                    if (notificacion != null)
                    {
                        return JsonConvert.SerializeObject(new { success = true, data = notificacion });
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new { success = false, message = "Notificación no encontrada." });
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
