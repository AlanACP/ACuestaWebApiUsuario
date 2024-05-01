using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace SL.Controllers
{
    public class UsuarioController : ApiController
    {
        [Route("Api/Usuario/Add")]
        [HttpPost]
        public IHttpActionResult Add([FromBody]ML.Usuario usuario)
        {
            var resultAdd = BL.Usuario.Add(usuario);

            if (resultAdd.Item1)
            {
                return Content(HttpStatusCode.OK, resultAdd);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, resultAdd);
            }
        }

        [Route("Api/Usuario/Update")]
        [HttpPut]
        public IHttpActionResult Update([FromBody ]ML.Usuario usuario)
        {
            var resultUpdate = BL.Usuario.Update(usuario);

            if (resultUpdate.Item1)
            {
                return Content(HttpStatusCode.OK, resultUpdate);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, resultUpdate);
            }
        }

        [Route("Api/Usuario/Delete")]
        [HttpDelete]
        public IHttpActionResult Delete(int IdUsuario)
        {
            var resultDelete = BL.Usuario.Delete(IdUsuario);   

            if (resultDelete.Item1)
            {
                return Content(HttpStatusCode.OK, resultDelete);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, resultDelete);
            }
        }

        [Route("Api/Usuario/GetAll")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            var resultGetAll = BL.Usuario.GetAll();

            if (resultGetAll.Item1)
            {
                usuario.Usuarios = resultGetAll.Item3;
                return Content(HttpStatusCode.OK, usuario);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, usuario);
            }
        }

        [Route("Api/Usuario/GetById")]
        [HttpGet]
        public IHttpActionResult GetById(int IdUsuario)
        {
            var result = BL.Usuario.GetById(IdUsuario);

            if (result.Item1)
            {
                return Content(HttpStatusCode.OK, result.Item3);
            }
            else
            {
                return Content (HttpStatusCode.NotFound, result.Item2);
            }
        }
    }
}
