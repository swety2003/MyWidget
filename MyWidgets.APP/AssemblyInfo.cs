using System.Windows;

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
                                     //(used if a resource is not found in the page,
                                     // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                              //(used if a resource is not found in the page,
                                              // app, or any theme specific resource dictionaries)
)]
[assembly: Git("{{buildtype}}", "{{githash}}")]



[System.AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = false)]
sealed class GitAttribute : System.Attribute
{
    public enum RelType
    {
        NightlyBuild,
        SelfBuild,
        Release,
        UnKnown,
    }

    readonly string githash;
    readonly string buildtype;

    // This is a positional argument
    public GitAttribute(string buildtype, string githash)
    {
        this.githash = githash;
        this.buildtype = buildtype;

    }

    public RelType GetRelType()
    {
        var s = this.ToString();
        if (s.Contains("dev"))
        {
            return RelType.NightlyBuild;
        }
        if (s.Contains("main"))
        {
            return RelType.Release;
        }
        if (s.Contains("{{buildtype}}"))
        {
            return RelType.SelfBuild;
        }

        return RelType.UnKnown;
        //switch (BuildType)
        //{
        //    case "refs/heads/dev": return RelType.NightlyBuild;
        //    case "refs/heads/main": return RelType.Release;
        //    case "{{buildtype}}": return RelType.SelfBuild;
        //    default:
        //        return RelType.UnKnown;
        //}
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