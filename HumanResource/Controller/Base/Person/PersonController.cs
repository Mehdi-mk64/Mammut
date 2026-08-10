using DAL.Repository;
using DAL.Repository.Basic.Personal;
using DAL.Repository.Basic.SMS;
using Entities.Basic.Personel;
using SystemManagment.Controller.Base;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace SMSAPI.Controller.Person
{
    public class PersonController : ApiControllerBase<Entities.Basic.Personel.Person>
    {
        

        public PersonController(IRepository<Entities.Basic.Personel.Person> repository) : base(repository)
        { 

        }


   



    }
}
