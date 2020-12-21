using CsvHelper;
using PersonApi.Core.Domain;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PersonApi.Persistence
{
    public class SeedPersonData
    {
        public static async Task SeedAync(PersonDbContext context)
        {
            var personData = context.Person.ToList();
            if(personData == null || personData.Count == 0)
            { 
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = "PersonApi.Seeddata.PersonData.csv";
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    using (var csvReader = new CsvReader(reader, CultureInfo.CurrentCulture))
                    {
                        csvReader.Configuration.HeaderValidated = null;
                        csvReader.Configuration.MissingFieldFound = null;
                        var records = csvReader.GetRecords<Person>().ToArray();

                        foreach (Person record in records)
                        {
                            context.Person.Add(record);
                        }
                        await context.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
