namespace ConsoleApp.Models
{
    public class Pilot : Person, ISkill
    {
        public string License { get; private set; }

        public Pilot(string firstName, string lastName, string license)
            : base(firstName, lastName)
        {
            License = license;
        }

        public override void Study()
        {

        }

        public void Skate()
        {
            
        }
    }
}
