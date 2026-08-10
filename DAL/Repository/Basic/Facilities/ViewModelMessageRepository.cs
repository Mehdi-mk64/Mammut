using Entities.Basic.Facilities;
using Entities.Basic.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;


namespace DAL.Repository.Basic.Facilities
{
    public class ViewModelMessageRepository : Repository<ViewModelMessage>
    {
        public ViewModelMessageRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public List<ViewModelMessage> GetDataList(ViewList viewList)
        {
            string queryStr = $"Select * From {viewList.SchemaName}.{viewList.ViewName} Where IsComlpete=@IsComlpete And HasError=@HasError ";
            byte isFalse = 0; 
            return Entities.FromSqlRaw(queryStr, new[] { new SqlParameter("@IsComlpete", isFalse), new SqlParameter("@HasError", isFalse) }).AsNoTracking<ViewModelMessage>().ToList();
        }

        public void UpdateDataList(ViewModelMessage viewModelMessage, ViewList view)
        {
            string queryStr = $"Update {view.SchemaName}.{view.ViewName} Set MessageSendID=@MessageSendID , IsComlpete=@IsComlpete, HasError=@HasError,DateInsert=@DateInsert  Where ID=@ID "; ;
            List<SqlParameter> parameters = new List<SqlParameter>() {
                        new SqlParameter("@MessageSendID",viewModelMessage.MessageSendID??0),
                        new SqlParameter("@ID", viewModelMessage.ID),
                        new SqlParameter("@HasError", viewModelMessage.HasError?1:0),
                        new SqlParameter("@IsComlpete", viewModelMessage.IsComlpete?1:0),
                        new SqlParameter("@DateInsert", DateTime.Now)

            };

            var newMessageList = DbContext.Database.ExecuteSqlRaw(queryStr, parameters);

        }



    }

}

