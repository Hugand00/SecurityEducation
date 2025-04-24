using SecurityEducationApi.Models;

namespace SecurityEducationApi.Repositories.Interface
{
	public interface ITestRepository
	{
		Task<Test> GetTestInfo(int id);
	}
}
