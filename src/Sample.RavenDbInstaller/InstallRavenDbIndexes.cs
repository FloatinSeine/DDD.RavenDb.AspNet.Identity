using System;
using System.Reflection;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Sample.Domain.Persistence.Raven.Customer.Transforms;

namespace Sample.RavenDbInstaller
{
    class InstallRavenDbIndexes
    {
        private static void Main(string[] args)
        {
            ConsolePrint("Installing RavenDB Indexes");
            ConsolePrint("Starting.....", true);
            using (var store = CreateDocumentStore())
            {
                store.Initialize();
                InstallIndexes(store);

            }
            ConsolePrint("....Completed");
            ConsolePrint("Press Enter to quit.");
            ConsolePause();
        }

        /// <summary>
        /// Creates the RavenDB Indexes and/or Transformers in the given datastore
        /// </summary>
        private static void InstallIndexes(IDocumentStore store)
        {
            var assembly = GetAssemblyWithIndexes();
            try
            {
                IndexCreation.CreateIndexes(assembly, store);
            }
            catch (Exception ex)
            {
                ConsolePrint("ERROR Creating Indexes");
                ConsolePrint(ex.Message);
            }
        }

        private static void ConsolePause()
        {
            Console.In.ReadLine();
        }

        private static void ConsolePrint(string message, bool? sameLine = false)
        {
            if (sameLine == true) Console.Out.Write(message);
            else Console.Out.WriteLine(message);
        }

        /// <summary>
        /// The assembly containing the RavenDB custom Indexes and/or Transformers
        /// </summary>
        private static Assembly GetAssemblyWithIndexes()
        {
            return Assembly.GetAssembly(typeof(CustomerAccountDetailsTransformer));
        }

        /// <summary>
        /// Creates a document store for the RavenDB to install the Indexes
        /// </summary>
        private static DocumentStore CreateDocumentStore()
        {
            var store = new DocumentStore
            {
                Url = "http://localhost:8080",
                DefaultDatabase = "Memberships"
            };

            return store;
        }
    }
}
