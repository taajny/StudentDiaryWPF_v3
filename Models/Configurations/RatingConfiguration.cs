using StudentDiaryWPF.Models.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentDiaryWPF.Models.Configurations
{
    public class RatingConfiguration : EntityTypeConfiguration<Rating>
    {
        public RatingConfiguration() 
        {
            ToTable("dbo.Ratings");

            HasKey(x => x.Id);


        }

    }
}
