# Asp.Net v4.5 Identity with RavenDB using a DDD approach

Created the following sample application to demonstrate using a domain driven design (DDD) approach to managing Customers Accounts and Logins, to create a very decoupled application

This approach uses Asp.Net v4.5 and Identity Framework v2.2.0 with RavenDB v3.0.3599.

Some simple modifications would allow you switch RavenDB for a different datastore, expan the event dispatch model to use messaging and event sourcing.

The web project has been remodelled to simplify managing the Account access features.

When signing in to the web application with Facebook the FB Access Token for the user is stored in a claims entry, which can be used to query the open graph.


## Step 1: Configuration for RavenDB


Update the web.config settings to point to your installation of RavenDB and the Database to use

```
  <connectionStrings>
    <add name="MembershipsDataStore" connectionString="URL=http://localhost:8080;Database=Memberships" />
  </connectionStrings>

```

If you change the name of the connection string in the web.config, you will also need to modify the name set in the Structuremap PersistenceRegistry to match the new name.


Open the file Sample.Domain.Persistence.Raven.DependencyResolution,PersistenceRegistry, and look for the following entry changing the "MembershipsDataStore" value to the new name.

```
            For<IRavenSessionFactoryBuilder>()
                .Use<RavenSessionFactoryBuilder>()
                .Ctor<string>("connectionString").Is("MembershipsDataStore").Named("MembershipStore");
```

Finally you will need to install the customer Transformers and Indexes. To do this open the Sample.RavenDbInstaller.InstallRavenDbInstaller file and modify the following section of the file with the correct connection details for your customer database

```
        private static DocumentStore CreateDocumentStore()
        {
            var store = new DocumentStore
            {
                Url = "http://localhost:8080",
                DefaultDatabase = "Memberships"
            };

            return store;
        }
```

Once modified, build the console application and run it.


## Step 2: Configuration for Facebook Authentication
To enable Facebook Authentication you will need to add your Facebook AppId and AppSecret codes to the following AppSetting keys in the web.config

```
    <add key="FacebookAppId" value="dfdsfsdsf" />
    <add key="FacebookAppSecret" value="sfsdfdfsfs" />
```

## Step 3: Namespace Changes
If you change the root namespace from Sample should also note will have to change the configuration in the following file.

In the Stracturemap Registry file Sample.Web.DependencyResolution.DefaultRegistry Scanning of assemblies is filtered on assmbly names containing Sample in the name to reduce the number of assembly files being scanned and therefore should be a slightly faster startup. Change the filter to the new namespace root if necessary.

```
       public DefaultRegistry() {
            Scan(
                scan => {
                    scan.TheCallingAssembly();
                    scan.AssembliesFromApplicationBaseDirectory(assembly=>assembly.FullName.Contains("Sample"));
                    scan.LookForRegistries();
                    scan.WithDefaultConventions();
                    scan.With(new ControllerConvention());
                });
            //For<IExample>().Use<Example>();
        }
```




--------

## About Stephen Williams

Writes at his blog [Agog in the Ether](http://agogintheether.blogspot.co.uk/).