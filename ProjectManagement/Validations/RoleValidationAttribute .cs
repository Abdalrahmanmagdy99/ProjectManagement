using ProjectManagement.DbContext;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.Validations
{
    public class RoleValidationAttribute : ValidationAttribute
    {
        private readonly string _roleName;

        public RoleValidationAttribute(string roleName)
        {
            _roleName = roleName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return new ValidationResult("User ID is required.");

            var userId = (int)value;
            var dbContext = validationContext.GetService<ProjectManagmentDbContext>();

            var roleId = dbContext.Roles
                .Where(r => r.Name == _roleName)
                .Select(r => r.Id)
                .FirstOrDefault();

            var isUserInRole = dbContext.UserRoles
                .Any(ur => ur.UserId == userId && ur.RoleId == roleId);

            if (!isUserInRole)
                return new ValidationResult($"User must have the role of {_roleName}.");

            return ValidationResult.Success;
        }
    }
}
