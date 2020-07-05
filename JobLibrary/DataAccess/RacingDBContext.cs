using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using JobLibrary.Models;
using JobLibrary.Models.Mapping;

namespace JobLibrary.DataAccess
{
    public class RacingDBContext : DbContext
    {
        static RacingDBContext()
        {
            Database.SetInitializer<RacingDBContext>(null);
        }
        public RacingDBContext()
            : base("Name=RacingDBContext") { }
        public DbSet<LotteryData> LotteryDatas { get; set; }
        public DbSet<NumeralGroup> NumeralGroups { get; set; }
        public DbSet<ContinuityNumber> ContinuityNumbers { get; set; }
        public DbSet<StatisticsTen> StatisticsTens { get; set; }
        public DbSet<StatisticsMonth> StatisticsMonths { get; set; }
        public DbSet<StatisticsThreeMonth> StatisticsThreeMonths { get; set; }
        public DbSet<StatisticsHalfYear> StatisticsHalfYears { get; set; }
        public DbSet<StatisticsYear> StatisticsYears { get; set; }
        public DbSet<Omissions> Omissions { get; set; }
        public DbSet<NoteUsers> NoteUsers { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new LotteryDataMap());
            modelBuilder.Configurations.Add(new NumeralGroupMap());
            modelBuilder.Configurations.Add(new ContinuityNumberMap());
            modelBuilder.Configurations.Add(new StatisticsTenMap());
            modelBuilder.Configurations.Add(new StatisticsMonthMap());
            modelBuilder.Configurations.Add(new StatisticsThreeMonthMap());
            modelBuilder.Configurations.Add(new StatisticsHalfYearMap());
            modelBuilder.Configurations.Add(new StatisticsYearMap());
            modelBuilder.Configurations.Add(new OmissionsMap());
            modelBuilder.Configurations.Add(new NoteUsersMap());
        }
    }
}