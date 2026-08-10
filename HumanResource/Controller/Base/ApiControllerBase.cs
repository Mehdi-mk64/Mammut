using Common.Exeptions;
using Entities.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebFramework;

namespace SystemManagment.Controller.Base
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public abstract class ApiControllerBase<TEntity> : ControllerBase
        where TEntity : class, IEntity
    {

        #region Fields

        protected readonly DAL.Repository.IRepository<TEntity> _repository;

        #endregion

        #region Constructors

        public ApiControllerBase(DAL.Repository.IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        #endregion

        #region Methods

        [HttpGet]
        public virtual async Task<IActionResult> Get(CancellationToken cancellationToken)
        {

            var res = await _repository.TableNoTracking.ToListAsync(cancellationToken);
            return Ok(res);

        }

        [HttpGet("{id:long}")]
        public virtual async Task<IActionResult> GetByID(long id, CancellationToken cancellationToken)
        {
            var model = await _repository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                return NotFound();
            return Ok(model);
        }

        [HttpPost]
        [Route("Create")]
        public virtual async Task<IActionResult> Create(TEntity instance, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(instance, cancellationToken);
            return Ok(instance);
        }

        [HttpPut]
        [Route("Edit/{id}")]
        public virtual async Task<IActionResult> Update(TEntity instance, long id, CancellationToken cancellationToken)
        {
            var model = await _repository.GetByIdAsync(cancellationToken, id);
            if (model == null)
                return NotFound();
            await _repository.UpdateAsync(instance, cancellationToken);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public virtual async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
        {
            var model = await _repository.GetByIdAsync(cancellationToken, id);
            await _repository.DeleteAsync(model, cancellationToken);
            return Ok();
        }

        #endregion
    }
}
