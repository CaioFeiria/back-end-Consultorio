using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace web_api.Controllers
{
    [EnableCors(origins:"*", headers:"*", methods:"*")]
    public class UsuariosController : ApiController
    {
        private readonly Repositories.SQLServer.Usuario repoUsuario;

        public UsuariosController()
        {
            repoUsuario = new Repositories.SQLServer.Usuario(Configurations.Database.GetConnectionString());
        }

        [HttpGet]
        // GET: api/Usuarios
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                return Ok(await this.repoUsuario.Select());
            }
            catch (Exception ex)
            {
                Utils.Logger.WriteException(Configurations.Logger.GetFullPath(), ex);

                return InternalServerError();
            }
        }

        [HttpGet]
        // GET: api/Usuarios/5
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                Models.Usuario usuario = await this.repoUsuario.Select(id);
                if (usuario == null)
                    return NotFound();

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                Utils.Logger.WriteException(Configurations.Logger.GetFullPath(), ex);

                return InternalServerError();
            }
        }

        [HttpGet]
        // GET: api/Usuarios?nome=aa
        public async Task<IHttpActionResult> Get(string nome)
        {
            try
            {
                return Ok(await this.repoUsuario.Select(nome));
            }
            catch (Exception ex)
            {
                Utils.Logger.WriteException(Configurations.Logger.GetFullPath(), ex);

                return InternalServerError();
            }
        }

        [HttpPost]
        // POST: api/Usuarios
        public async Task<IHttpActionResult> Post([FromBody]Models.Usuario usuario)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Content(HttpStatusCode.Created, await this.repoUsuario.Insert(usuario));
            }
            catch (Exception ex)
            {
                Utils.Logger.WriteException(Configurations.Logger.GetFullPath(), ex);

                return InternalServerError();
            }
        }

        [HttpPut]
        // PUT: api/Usuarios/5
        public async Task<IHttpActionResult> Put(int id, [FromBody] Models.Usuario usuario)
        {
            try
            {
                if(!Validations.Requisicao.IdRequisicaoIgualAoIdCorpoRequisicao(usuario.Id, id))
                    return BadRequest("O id do corpo não coincide com o id da requisição.");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!await this.repoUsuario.Update(usuario))
                    return NotFound();

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                Utils.Logger.WriteException(Configurations.Logger.GetFullPath(), ex);

                return InternalServerError();
            }
        }

        [HttpDelete]
        // DELETE: api/Usuarios/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                if (!await this.repoUsuario.Delete(id))
                    return NotFound();

                return Ok(id);
            }
            catch (Exception ex)
            {
                Utils.Logger.WriteException(Configurations.Logger.GetFullPath(), ex);

                return InternalServerError();
            }
        }
    }
}
