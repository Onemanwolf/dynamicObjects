using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.Text;


namespace dynamicObjects;

class Program
{
    static void Main(string[] args)
    {

    string json = "{ \"className\": \"User\", \"properties\": { \"Name\": \"string\", \"Email\": \"string\", \"IsActive\": \"bool\" } }";
    dynamic obj = JsonConvert.DeserializeObject<ExpandoObject>(json);

    IDictionary<string, object> properties = obj as IDictionary<string, object>;
    IDictionary<string, object> propertyPairs = properties["properties"] as IDictionary<string, object>;

    var className = properties["className"].ToString();

    StringBuilder classCode = new StringBuilder();
    classCode.AppendLine("namespace dynamicObjects.Models");
    classCode.AppendLine("{");
    classCode.AppendLine("public class " + className);
    classCode.AppendLine("{");

    foreach (var propertyPair in propertyPairs)
    {
        classCode.AppendLine($"    public {propertyPair.Value} {propertyPair.Key} {{ get; set; }}");
    }

    classCode.AppendLine("}");
    classCode.AppendLine("}");

    System.IO.File.WriteAllText(@"C:\Users\timot\source\repos\dynamicObjects\Models\" + className + ".cs", classCode.ToString());
    }
}
