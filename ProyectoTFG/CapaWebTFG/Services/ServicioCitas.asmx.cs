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

namespace CapaWebTFG.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    public class ServicioCitas : WebService
    {
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string ObtenerCitas()
        {
            try
            {
                using (var context = new Contexto())
                {
                    var citas = context.Citas
                        .Select(c => new
                        {
                            c.Citas_Id,
                            c.Fecha,
                            c.Motivo,
                            c.Persona_Id
                        }).ToList();

                    return JsonConvert.SerializeObject(new { success = true, data = citas });

                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { success = false, message = ex.Message });
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string BorrarCita(int id)
        {
            try
            {
                using (var context = new Contexto())
                {
                    var citas = context.Citas.Find(id);
                    if (citas != null)
                    {
                        context.Citas.Remove(citas);
                        context.SaveChanges();
                        return JsonConvert.SerializeObject(new { success = true });
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new { success = false, message = "Cita no encontrada." });
                    }
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { success = false, message = ex.Message });
            }
        }
        [WebMethod]
        public string AgregarCita(DateTime Fecha, string Motivo, int Persona_Id)
        {
            try
            {
                // Crear un nuevo objeto cita
                var nuevaCita = new Citas
                {
                    Fecha = Fecha,
                    Motivo = Motivo,
                    Persona_Id = Persona_Id,
                };

                // Validar la entidad Cita
                var validationResults = new List<ValidationResult>();
                var validationContext = new ValidationContext(nuevaCita);
                if (!Validator.TryValidateObject(nuevaCita, validationContext, validationResults, true))
                {
                    return JsonConvert.SerializeObject(new { success = false, message = "Validación fallida: " + string.Join(", ", validationResults.Select(v => v.ErrorMessage)) });
                }

                // Añadir la cita a la base de datos
                using (var context = new Contexto())
                {
                    context.Citas.Add(nuevaCita); // Cambiado a context.Citas
                    context.SaveChanges();
                }

                return JsonConvert.SerializeObject(new { success = true });
            }
            catch (ArgumentNullException ex)
            {
                return JsonConvert.SerializeObject(new { success = false, message = "Un parámetro requerido es nulo: " + ex.ParamName });
            }
            catch (Exception ex)
            {
                // Loguear el error en algún sistema de logging para auditoría
                return JsonConvert.SerializeObject(new { success = false, message = "Ha ocurrido un error inesperado: " + ex.Message });
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public string EditarCita(int citas_Id, DateTime fecha, string motivo, int persona_Id)
        {
            try
            {
                using (var context = new Contexto())
                {
                    var cita = context.Citas.Find(citas_Id);
                    if (cita != null)
                    {
                        cita.Citas_Id = citas_Id;
                        cita.Fecha = fecha;
                        cita.Motivo = motivo;
                        cita.Persona_Id = persona_Id;


                        context.SaveChanges();
                        return JsonConvert.SerializeObject(new { success = true });
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new { success = false, message = "Cita no encontrada." });
                    }
                }
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { success = false, message = ex.Message });
            }
        }
        // Método para hashear la contraseña usando BCrypt
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }




        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string ObtenerCitaPorId(int id)
        {
            try
            {
                using (var context = new Contexto())
                {
                    var cita = context.Citas.Find(id);

                    if (cita != null)
                    {
                        return JsonConvert.SerializeObject(new { success = true, data = cita });
                    }
                    else
                    {
                        return JsonConvert.SerializeObject(new { success = false, message = "Cita no encontrada." });
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
