param(
    [object] $WebhookData   
)
$VerbosePreference = 'continue'

Import-Module MicrosoftTeams

$input = (ConvertFrom-Json -InputObject $WebhookData.RequestBody)

$AzureOrgIdCredential = "TeamsCredential"
$Cred = Get-AutomationPSCredential -Name $AzureOrgIdCredential
 
Connect-MicrosoftTeams -Credential $Cred
 
$newTeam = New-Team -DisplayName $input.DisplayName -Alias $input.Alias -AccessType "private" -AddCreatorAsMember $false

Add-TeamUser -GroupId $newTeam.GroupId -User $input.Owner -Role Owner

 $input.Members.Split(",") | ForEach {
   Add-TeamUser -GroupId $newTeam.GroupId -User $_ -Role Member
 }

Set-TeamFunSettings -GroupId $newTeam.GroupId -AllowCustomMemes true

if ($input.Purpose -eq "Marketing")
{
    New-TeamChannel -GroupId $newTeam.GroupId -DisplayName "Presentations"
    New-TeamChannel -GroupId $newTeam.GroupId -DisplayName "Website Launch"
    New-TeamChannel -GroupId $newTeam.GroupId -DisplayName "Announcements"
}

if ($input.Purpose -eq "Project")
{
    New-TeamChannel -GroupId $newTeam.GroupId -DisplayName "Product Launch"
    New-TeamChannel -GroupId $newTeam.GroupId -DisplayName "Sprints"
    New-TeamChannel -GroupId $newTeam.GroupId -DisplayName "Deploy"
}