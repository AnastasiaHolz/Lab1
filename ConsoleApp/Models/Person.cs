namespace ConsoleApp.Models
{
    public abstract class Person
    {
        public string FirstName { get; protected set; }
        public string LastName  { get; protected set; }

        protected Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName  = lastName;
        }

        public abstract void Study();
    }
}
