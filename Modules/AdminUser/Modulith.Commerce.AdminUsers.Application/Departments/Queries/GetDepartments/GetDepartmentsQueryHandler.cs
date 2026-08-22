using Modulith.Commerce.AdminUser.Domain.Departments;
using Modulith.Commerce.Common.Application.Abstractions.Messaging;
using Modulith.Commerce.Common.Domain.Abstractions;

namespace Modulith.Commerce.AdminUsers.Application.Departments.Queries.GetDepartments
{
    public class GetDepartmentsQueryHandler(IDepartmentsRepository departmentsRepository)
        : IQueryHandler<GetDepartmentsQuery, List<GetDepartmentsItemResponse>>
    {
        public async Task<Result<List<GetDepartmentsItemResponse>>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var options = new FilteringOptions<Department>
            {
                Relations = ["Head", "Parent"]
            };

            if (!string.IsNullOrWhiteSpace(request.Key))
            {
                string key = request.Key.Trim();
                options.Predicates.Add(d => d.Name.Value.Contains(key));
            }

            var departments = await departmentsRepository.SelectAsync(options, cancellationToken);

            var response = departments
                .AsEnumerable()
                .Select(d => new GetDepartmentsItemResponse(
                    d.Id,
                    d.Name.Value,
                    d.Parent?.Name.Value,
                    $"{d.Head.FirstName.Value} {d.Head.LastName.Value}",
                    d.ModifiedDate))
                .ToList();

            return Result.Success(response);
        }
    }
}
