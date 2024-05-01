using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web;
using System.Web.Mvc;


namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44337/Api/");

                    var responseTask = client.GetAsync("Usuario/GetAll");
                    responseTask.Wait();

                    var resultService = responseTask.Result;

                    if (resultService.IsSuccessStatusCode)
                    {
                        var readTask = resultService.Content.ReadAsAsync<ML.Usuario>();
                        readTask.Wait();

                        usuario = readTask.Result;
                        return View(usuario);
                    }
                    else
                    {
                        return View(usuario);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Text = ex.Message;
                return PartialView("Modal");
            }
        }

        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            if (IdUsuario != null)
            {
                //UPDATE
                try
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:44337/Api/");

                        var responseTask = client.GetAsync("Usuario/GetById?IdUsuario=" + IdUsuario);
                        responseTask.Wait();

                        var resultService = responseTask.Result;

                        if (resultService.IsSuccessStatusCode)
                        {
                            var readTask = resultService.Content.ReadAsAsync<ML.Usuario>();
                            responseTask.Wait();

                            usuario = readTask.Result;
                            return View(usuario);
                        }
                        else
                        {
                            ViewBag.Text = "Error al recuperar el registro";
                            return PartialView("Modal");
                        }
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Text = ex.Message;
                    return PartialView("Modal");
                }
            }
            else
            {
                //ADD
                return View(usuario);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (usuario.IdUsuario != 0)
                {
                    //UPDATE
                    try
                    {
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri("https://localhost:44337/Api/");

                            var responseTask = client.PutAsJsonAsync("Usuario/Update", usuario);

                            var resultService = responseTask.Result;

                            if (resultService.IsSuccessStatusCode)
                            {
                                ViewBag.Text = "Usuario actualizado correctamente";
                                return PartialView("Modal");
                            }
                            else
                            {
                                ViewBag.Text = "Error al actualizar el usuario";
                                return PartialView("Modal");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Text = ex.Message;
                        return PartialView("Modal");
                    }
                }
                else
                {
                    //ADD
                    try
                    {
                        using (var client = new HttpClient())
                        {
                            client.BaseAddress = new Uri("https://localhost:44337/Api/");
                            var responseTask = client.PostAsJsonAsync("Usuario/Add", usuario);
                            responseTask.Wait();

                            var resultService = responseTask.Result;

                            if (resultService.IsSuccessStatusCode)
                            {
                                ViewBag.Text = "Usuario agregado correctamente";
                                return PartialView("Modal");
                            }
                            else
                            {
                                ViewBag.Text = "Error al agregar el usuario";
                                return PartialView("Modal");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Text = ex.Message;
                        return PartialView("Modal");
                    }
                }
            }
            else
            {
                return View(usuario);
            }
            
        }

        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44337/Api/");
                    var responseTask = client.DeleteAsync("Usuario/Delete?IdUsuario=" + IdUsuario);
                    responseTask.Wait();

                    var resultService = responseTask.Result;

                    if (resultService.IsSuccessStatusCode)
                    {
                        ViewBag.Text = "Usuario eliminado correctamente";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Text = "Error al eliminar el usuario";
                        return PartialView("Modal");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Text = ex.Message;
                return PartialView("Modal");
            }
        }
    }
}