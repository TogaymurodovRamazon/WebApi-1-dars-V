using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_1_dars_V.IRepository;
using WebApi_1_dars_V.Model;

namespace WebApi_1_dars_V.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IGenericRepository<User> repository;
        public UserController(IGenericRepository<User> repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public IQueryable<User> GetAll()
       => repository.GetAll(null);

        [HttpPost]
        public async Task<User> Create(User user)
        {
           var res= await repository.CreateAsync(user);
            repository.SaveChangesAsync();
       return res;
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await repository.DeleteAsync(p=>p.Id == id);
            repository.SaveChangesAsync();
        }

        [HttpPut]
        public async Task Put(User user)
        {
             repository.Update(user);
            repository.SaveChangesAsync();
        }
    }
}
