namespace ConsoleApp.Models
{
    public class Musician : Person, ISkill
    {
        public string Instrument { get; private set; }

        public Musician(string firstName, string lastName, string instrument)
            : base(firstName, lastName)
        {
            Instrument = instrument;
        }

        public override void Study()
        {
        
        }

        public void Skate()
        {
            Console.WriteLine("Катається на ковзанах");
        }
    }
}
