Param(
	[Parameter(Position=0)]
	[string]$psakePath = '.\packages\psake.4.2.0.1\tools\psake.psm1'
)

if( Get-Command -Name 'Invoke-psake' -Type Function -ErrorAction SilentlyContinue )
{
	Write-Host 'Cheers! psake is already loaded'
}
else
{
	Write-Host 'Loading psake, don''t not get to drunk'
	
	Import-Module $psakePath
}
