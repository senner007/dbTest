namespace dbTest
{
    public class Student : Person
    {
        public string Major { get; set; }
        public override string Status { get; set; } = "Student";
    }
}
