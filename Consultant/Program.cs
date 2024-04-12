using Consultant.Models;
using Microsoft.EntityFrameworkCore;

namespace Consultant
{
	class Program
	{
		private const int _batchSize = 10000;
		private const int _size = 10000000;

		static void Main(string[] args)
		{
			using (var context = new DatabaseContext())
			{
				context.Database.Migrate();

				for (int i = 0; i < _size; i += _batchSize)
				{
					var numbers = new List<Number>();

					for (int j = 0; j < _batchSize && i + j < _size; j++)
					{
						numbers.Add(new Number { Value = new Random().Next(1, 100001) });
					}
					context.BulkInsert(numbers);
				}
			}
		}
	}
}

