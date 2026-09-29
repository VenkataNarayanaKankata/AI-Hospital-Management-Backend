namespace HospitalManagement.Api.Entities
{
    public class Role
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; } = string.Empty;

        public ICollection<Admin> Admins { get; set; } = new List<Admin>();
    }
}