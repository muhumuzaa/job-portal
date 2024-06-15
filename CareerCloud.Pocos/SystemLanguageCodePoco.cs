using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("System_Language_Codes")]
    public class SystemLanguageCodePoco : IPoco
    {
        public Guid Id { get; set; }
        [Key]
        public string LanguageID { get; set; }

        [Column("Native_Name")]
        public string NativeName { get; set; }

        public string Name { get; set; }

        public virtual ICollection<CompanyDescriptionPoco> CompanyDescriptions { get; set; }  //done
    }
}
