using GlobalApi;
using ClientData;
using NJsonSchema;
using NJsonSchema.CodeGeneration.CSharp;

namespace ApiGenerator
{
    internal static class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Schema:\n\n");
            JsonSchema schema = JsonSchema.FromType<GetPlayersCommand>();
            string schemaString = schema.ToJson();

            Console.WriteLine(schemaString);
            Console.WriteLine("\n\nC#:\n\n");

            JsonSchema loadedSchema = await JsonSchema.FromJsonAsync(schemaString);
            CSharpGenerator generator = new CSharpGenerator(loadedSchema);
            Console.WriteLine(generator.GenerateFile());
        }
    }
}