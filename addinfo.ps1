(Get-Content MyWidgets.APP\AssemblyInfo.cs) -Replace '{{buildtype}}', $Env:GITHUB_REF | Set-Content MyWidgets.APP\AssemblyInfo.cs
(Get-Content MyWidgets.APP\AssemblyInfo.cs) -Replace '{{githash}}', $Env:GITHUB_SHA | Set-Content MyWidgets.APP\AssemblyInfo.cs