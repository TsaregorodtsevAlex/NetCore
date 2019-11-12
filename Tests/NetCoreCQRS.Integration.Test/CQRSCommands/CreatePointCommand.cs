using NetCoreCQRS.Commands;
using NetCoreCQRS.Integration.Test.Model;
using System.Threading.Tasks;

namespace NetCoreCQRS.Integration.Test.CQRSCommands
{
	public class CreatePointCommand : BaseCommand<NetCoreCQRSDbContext>
	{
		public async Task Execute(Point point)
		{
			Context.Points.Add(point);

			await Context.SaveChangesAsync();
		}
	}
}
