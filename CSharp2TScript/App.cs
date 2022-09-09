using CSharp2TScript.Service.Interface;

namespace CSharp2TScript
{
    public class App
    {
        private readonly IFormatService _formatService;
        public App(IFormatService formatService)
        {
            _formatService = formatService;
        }

        public void Run()
        {
            string cSharp = @"public class PersonDto
                            {
                                public string Name { get; set; }
                                public int Age { get; set; }
                                public string Gender { get; set; }
                                public long? DriverLicenceNumber { get; set; }
                                public List<Address> Addresses { get; set; }
                                public class Address
                            {
                                public int StreetNumber { get; set; }
                                public string StreetName { get; set; }
                                public string Suburb { get; set; }
                                public int PostCode { get; set; }
                            }
                            }";

            string tScript = _formatService.CSharp2TScript(cSharp);
            Console.WriteLine("Result=>");
            Console.WriteLine(tScript);
        }
    }
}
