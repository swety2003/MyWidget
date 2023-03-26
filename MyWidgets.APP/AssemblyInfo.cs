using System.Windows;

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]
[assembly: Git("{{buildtype}}","{{githash}}")]



[System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = false)]
sealed class GitAttribute : System.Attribute
{

    readonly string githash;
    readonly string buildtype;

    // This is a positional argument
    public GitAttribute(string buildtype,string githash)
    {
        this.githash = githash;
        this.buildtype = buildtype;

    }

    public string Hash
    {
        get { return githash; }
    }

    public string BuildType
    {
        get { return buildtype; }
    }

    public override string ToString()
    {
        return $"{buildtype}-{githash}";
    }

}