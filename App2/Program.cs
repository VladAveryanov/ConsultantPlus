using Consultant;
using Consultant.Models;
using Microsoft.EntityFrameworkCore;

namespace App2
{
	class Program
	{
		static async Task Main(string[] args)
		{
			using (var context = new DatabaseContext())
			{
				var sql = @"INSERT INTO NumberStatistics (Count, Value)
							SELECT COUNT(Value) AS Count, Value
							FROM Numbers
							GROUP BY Value;";
				context.Database.ExecuteSqlRaw(sql);

				var list = await context.NumberStatistics
					.OrderByDescending(n => n.Count)
					.Take(10)
					.ToListAsync();
				
				DisplayHistogram(list.OrderBy(q => q.Count).ToList());
			}
		}

		static void DisplayHistogram(List<NumberStatistic> statistics)
		{
			var dict = new Dictionary<int, string>()
			{
				{ 0, new string('*', 1) },
				{ 1, new string('*', 2) },
				{ 2, new string('*', 3) },
				{ 3, new string('*', 4) },
				{ 4, new string('*', 5) },
				{ 5, new string('*', 6) },
				{ 6, new string('*', 7) },
				{ 7, new string('*', 8) },
				{ 8, new string('*', 9) },
				{ 9, new string('*', 10) },
			};

			for (int i = 0; i < statistics.Count; i++)
			{
				Console.WriteLine($"{statistics[i].Value}: {dict[i]} {statistics[i].Count}");
			}
		}
	}
}

