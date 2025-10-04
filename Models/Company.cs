namespace CRUDUsingDapper.Models
{
    public class Company
    {
        public int Id { get; set; }           // Primary Key
        public string Name { get; set; }      // اسم الشركة
        public string Address { get; set; }   // عنوان الشركة
        public string Country { get; set; }   // الدولة

        // علاقة One-to-Many: كل شركة ممكن يكون ليها أكتر من موظف
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }

}
