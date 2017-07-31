using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using Acquaint.Service.DataObjects;
using Acquaint.Service.Models;

namespace Acquaint.Service.Controllers
{
    public class ClientController : TableController<Client>
    {
        private FacePicDbContext _DataContext;
        
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            _DataContext = new FacePicDbContext();
            DomainManager = new EntityDomainManager<Client>(_DataContext, Request, enableSoftDelete: true);
        }

        // GET tables/Client/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public IQueryable<Client> GetAllClients()
        {
            return Query();
        }

        // GET tables/Client/3BF73C70-1DEE-47DC-8150-3A9BF368F01E
        public SingleResult<Client> GetClient(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Client/3BF73C70-1DEE-47DC-8150-3A9BF368F01E
        public async Task<Client> PatchClient(string id, Delta<Client> patch)
        {
            return await UpdateAsync(id, patch);
        }

        // POST tables/Client
        public async Task<IHttpActionResult> PostClient(Client item)
        {
            Client current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Client/3BF73C70-1DEE-47DC-8150-3A9BF368F01E
        public async Task DeleteClient(string id)
        {
            await DeleteAsync(id);
        }
    }
}