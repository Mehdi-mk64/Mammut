using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Basic.Facilities
{
    public class InsertDataLog : Base.BaseEntities<long>
    {
        #region Properties
        public DateTime DateTimeInsert { get; set; }
        public bool IsCompleted { get; set; }
        public string Descpription { get; set; }
        public long ContRecord { get; set; }

        public long ViewListID { get; set; }

        public ViewList InsertDataLog_ViewList { get; set; }

        #endregion

        #region Configuration
        public class InsertDataLogConfiguration : IEntityTypeConfiguration<InsertDataLog>
        {
            public void Configure(EntityTypeBuilder<InsertDataLog> builder)
            {
                builder.ToTable("InsertDataLog", "Service");

                builder.HasOne(o => o.InsertDataLog_ViewList).WithMany(m => m.ViewList_InsertDataLog).HasForeignKey(f => f.ViewListID).HasConstraintName("FK_ViewList_InsertDataLog_ID");
            }
        }
        #endregion



    }
  
}
