properties {
	$BaseDir = Resolve-Path '.'
    $SolutionFile = 'Sleddog.ExtendedTrail.sln'
	$SolutionDir = 'Sleddog.ExtendedTrail'
	$BuildConfig = 'Debug'
	$BuildDir
	$MSBuildOutput = 'minimal'
	$7z = "$Env:ProgramW6432\7-Zip\7z.exe"
	$xunitrunner = '.\packages\xunit.runners.1.9.1\tools\xunit.console.clr4.exe'
}

Framework '4.0'

Task default -Depends Compile

Task Test -Depends Compile {
	$testAssemblies = Get-ChildItem -Recurse -Filter *Tests.dll $BuildDir
	
	foreach( $ta in $testAssemblies )
	{
		Exec { & $xunitrunner $ta.FullName /html "$BuildDir\testoutput.htm" }
	}
}

Task Compile -Depends Clean {
	$buildArgs = FormatMsBuildArgs $SolutionFile $BuildConfig $BuildDir $SolutionDir

	Exec { msbuild $buildArgs }
}

Task Clean -Depends  Init {
	Write-Host "Removing old build files"

	Remove-Item -Force -Recurse $BuildDir -ErrorAction SilentlyContinue
}

Task Init {
	$script:BuildDir = $BaseDir.Path + "\Build\Automated\" + $BuildConfig
}

function FormatMsBuildArgs( $solution, $configuration, $buildDir, $moduleDir )
{
	$OutDir = FormatOutDir "$buildDir\$moduleDir\"
	
	"""$solution"" /nologo /v:$MSBuildOutput /t:Rebuild /p:Configuration=$configuration;OutDir=$OutDir"
}

function FormatOutDir($inputPathString)
{
	if( ($inputPathString -match '\s') -eq $true )
	{
		return """$($inputPathString)\""";
	}

	return $inputPathString
}