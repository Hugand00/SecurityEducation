using SecurityEducationApi.Dtos;

namespace SecurityEducationApi.Services.Interface
{
	public interface ITestService
	{
		Task<TestDto> GetTestInfoFromID(int id);
	}
}
