using DAL.Repository.Basic.SMS;
using DAL;
using Entities.Basic.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository.Basic.Facilities;
using System.Threading;

namespace Services.View
{
    public class ViewInsert
    {
        private AppDbContext dbContext;
        private ViewListRepository _viewListRepository;
        public ViewInsert()
        {

            dbContext = new AppDbContext();
            _viewListRepository = new ViewListRepository(dbContext);
        }
        public async Task InsertDataFromView()
        {
            CancellationToken cancellationToken = new CancellationToken();
            await _viewListRepository.InsertDataFromView(cancellationToken);
        }

    }
}
