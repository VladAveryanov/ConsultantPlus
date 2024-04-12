using Microsoft.EntityFrameworkCore;

namespace Consultant
{
	public class DatabaseContext : DbContext
	{
		private const string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=RandomNumbers;Trusted_Connection=True;";

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(_connectionString);
		}

		public DbSet<Number> Numbers { get; set; }
		public DbSet<NumberStatistic> NumberStatistics { get; set; }
	}
}
