using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CareerCloud.Pocos;

namespace CareerCloud.WebAPI.Data
{
    public class CareerCloudWebAPIContext : DbContext
    {
        public CareerCloudWebAPIContext (DbContextOptions<CareerCloudWebAPIContext> options)
            : base(options)
        {
        }

        public DbSet<CareerCloud.Pocos.SystemCountryCodePoco> SystemCountryCodePoco { get; set; } = default!;
        public DbSet<CareerCloud.Pocos.SecurityRolePoco> SecurityRolePoco { get; set; } = default!;
        public DbSet<CareerCloud.Pocos.SecurityLoginsRolePoco> SecurityLoginsRolePoco { get; set; } = default!;
        public DbSet<CareerCloud.Pocos.ApplicantEducationPoco> ApplicantEducationPoco { get; set; } = default!;
        public DbSet<CareerCloud.Pocos.ApplicantJobApplicationPoco> ApplicantJobApplicationPoco { get; set; } = default!;
        public DbSet<CareerCloud.Pocos.ApplicantProfilePoco> ApplicantProfilePoco { get; set; } = default!;
        public DbSet<CareerCloud.Pocos.ApplicantResumePoco> ApplicantResumePoco { get; set; } = default!;
        public DbSet<CareerCloud.Pocos.ApplicantSkillPoco> ApplicantSkillPoco { get; set; } = default!;
        public DbSet<CareerCloud.Pocos.ApplicantWorkHistoryPoco> ApplicantWorkHistoryPoco { get; set; } = default!;
        public DbSet<CareerCloud.Pocos.CompanyDescriptionPoco> CompanyDescriptionPoco { get; set; } = default!;
        public DbSet<CareerCloud.Pocos.CompanyJobPoco> CompanyJobPoco { get; set; } = default!;
        public DbSet<CareerCloud.Pocos.CompanyJobDescriptionPoco> CompanyJobDescriptionPoco { get; set; } = default!;
        public DbSet<CareerCloud.Pocos.CompanyJobEducationPoco> CompanyJobEducationPoco { get; set; } = default!;
    }
}
